
const UI = {
    authTabs : document.getElementById("auth-tabs"),
    formTitle : document.getElementById("form-title"),
    formBtn : document.getElementById("form-btn"),
    txbEmail : document.getElementById("txb-email"),
    txbPassword : document.getElementById("txb-password"),
    authContainer : document.getElementById("auth-container"),
    schoolPanel : document.getElementById("school-panel"),
    toast : document.getElementById("toast"),
    nameId : document.getElementById("email"),
    role :document.getElementById("role"),
    logout : document.getElementById("logout"),
    actionPanel :document.getElementById("action-panel"),
    addBtn : document.getElementById("add-btn"),
    panel : document.getElementById("overlay"),
    dataTable : document.getElementById("data")
};
const methods ={Post : "POST",Get : "GET" , Put : "PUT",Del : "DELETE"};
const Api = `http://localhost:5017/api`;
let CurrentUser = null;
let data = [];


UI.authTabs.addEventListener("click",(e)=>{
    const btn = e.target.closest("button");
    if(!btn) return;
    [...UI.authTabs.querySelectorAll("button")].forEach((b)=>b.classList.remove("active"));
    btn.classList.add("active"); 
    const type = btn.dataset.filter;
    UI.formTitle.textContent =UI.formBtn.textContent = UI.formBtn.dataset.action = type;
});
const Token = {
    saveToken(token){
        document.cookie = `token=${token}; path=/; max-age:1800; Secure; SameSite=Strict`
    },
    getToken(){
        return document.cookie
        .split("; ")
        .find(row => row.startsWith("token" + "="))
        ?.split("=")[1];
    },
    removeToken(){
        document.cookie = "token=; max-age=0; path=/";
    }
};
async function RequestApi(c,method = methods.Get, body){

    let option = {method : method,headers:{
        "Content-Type": "application/json",
        "Authorization" : `Bearer ${Token.getToken()}`
    }};
    if(body){
        option.body= JSON.stringify(body);
    }
    try{
        const res = await  fetch(`${Api}/${c}`,option);
        if(!res.ok){
             throw new Error(`Request failed with status ${res.status}`);
        }
        return await res.json();
    }
    catch(e){
        showToast("Error Happend!");
        console.error("Fetch failed:", e);

    }
}
function showToast(message){
    UI.toast.textContent= message;
    UI.toast.style.display = "block";
    setTimeout(()=> {UI.toast.style.display= "none"},2200)
}
function validateFrom(){
    const email = UI.txbEmail.value.trim();
    const password = UI.txbPassword.value.trim();
    if(!email)
        return {ok : false , message : "email is required"};
    if(password.length < 8)
        return {ok : false , message : "Password must be greather than 8"};
    return {ok : true , data : {email : email , password : password}};
}
async function handleLogin(){
    const result = validateFrom();
    if(!result.ok){
        showToast(result.message);
        return;
    }
    const data = await RequestApi("Auth/Login",methods.Post,result.data);
    Token.saveToken(data.value.accessKey);
}
async function handleRegister(){
     const result = validateFrom();
    if(!result.ok){
        showToast(result.message);
        return;
    }
    const data = await RequestApi("Auth/Register",methods.Post,result.data);
}
UI.formBtn.addEventListener("click",async (e)=>{
   const btn = e.target.dataset.action ;
   if(btn ==="Login")
        await handleLogin();
    if(btn === "Register" )
        await handleRegister();
    render();

    UI.txbEmail.value ="";
    UI.txbPassword.value = "";
});
UI.logout.addEventListener("click",()=>{
    if(!confirm("do you want to logout!"))return;
    Token.removeToken();
    render();
}
);
function checkToken(){
    const token = Token.getToken();
    return !token ||token.trim() === "" ;
}
const GetRequest = {
    async GetAllStudents(){
       const data =  await RequestApi("Student/GetAllStudents");
       console.log(data);
       return data.value;
    }
};

UI.actionPanel.addEventListener("click",async(e)=>{
    const btn = e.target.closest("button");
    if(!btn) return;
    [...UI.actionPanel.querySelectorAll("button")].forEach(ele => ele.classList.remove("active"));
    btn.classList.add("active");
    const f = btn.dataset.api;
    UI.addBtn.querySelector("span").textContent = f;
    if(f === "Student") data= await GetRequest.GetAllStudents();

});

UI.addBtn.addEventListener("click",(e)=>{
    UI.panel.style.display= "flex";
});
UI.panel.addEventListener("click",(e)=>{
    if(e.target.closest(".cancel")){
        UI.panel.style.display= "none";
    }
    if(e.target.closest("button")){
        UI.panel.style.display= "none";
    }
});

function renderPanel(){
    UI.addBtn.style.visibility = "hidden";
    if(CurrentUser.role === "Admin"){
        UI.addBtn.style.visibility = "visible";
        UI.actionPanel.innerHTML = `
         <div class="row ">
          <button data-api="Student" class="secondary active" type="button">Students</button>
          <button data-api="Teacher" class="secondary" type="button">Teachers</button>
          <button data-api="Class" class="secondary" type="button">Classes</button>
          <button data-api="Subject" class="secondary" type="button">Subjects</button>
          <button data-api="Grade" class="secondary" type="button">Grades</button>
          <button data-api="Department" class="secondary" type="button">Departments</button>
        </div>
        `;
        return;
    }
    if(CurrentUser.role === "User"){
        UI.actionPanel.innerHTML = `
            <div class="row ">
                <button data-api="Student" class="secondary active" type="button">Student</button>
            </div>
        `;
        return;

    }
    if(CurrentUser.role === "Student"){
        UI.actionPanel.innerHTML = `
            <div class="row ">
            <button data-api="Student" class="secondary active" type="button">Students</button>
          <button data-api="Class" class="secondary" type="button">Classes</button>
          <button data-api="Subject" class="secondary" type="button">Subjects</button>
          <button data-api="Grade" class="secondary" type="button">Grades</button>
           </div>
        `;
        return;

    }
    if(CurrentUser.role === "Teacher"){
        UI.actionPanel.innerHTML = `
          <div class="row ">
            <button data-api="Student" class="secondary active" type="button">Students</button>
          <button data-api="Teacher" class="secondary" type="button">Teachers</button>
          <button data-api="Class" class="secondary" type="button">Classes</button>
          <button data-api="Subject" class="secondary" type="button">Subjects</button>
          <button data-api="Grade" class="secondary" type="button">Grades</button>
            </div>
    `;
        return;

    }
}
function render(){
    if(checkToken()){
        UI.authContainer.classList.remove("nDisplay");
        UI.schoolPanel.classList.add("nDisplay");
        return;
    } 
    GetUserInfo();
    UI.authContainer.classList.add("nDisplay");
    UI.schoolPanel.classList.remove("nDisplay");
    UI.nameId.textContent = (CurrentUser.email);
    UI.role.textContent = (CurrentUser.role) ;
    renderPanel();
    if(data.length ===0) showToast("There is no data");
    UI.dataTable.innerHTML = data.map(e=>{
        `
         <table>
    <thead>
      <tr>
        <th>FullName</th>
        <th>Gender</th>
        <th>Date Of Birth</th>
        <th>Enrollment Date</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>${e.fullName}</td>
        <td>${e.gender}</td>
        <td>2000-01-01</td>
        <td>2023-09-01</td>
      </tr>
    </tbody>
  </table>

        
        `
    });
    
}  

function GetUserInfo(){
    const token = Token.getToken();
    const payload = JSON.parse(atob(token.split('.')[1]));
    const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    const email = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
    CurrentUser = {email : email, role : role}
}

(function boot(){
    render();
})();