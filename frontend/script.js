const UI = {
  authTabs: document.getElementById("auth-tabs"),
  formTitle: document.getElementById("form-title"),
  formBtn: document.getElementById("form-btn"),
  txbEmail: document.getElementById("txb-email"),
  txbPassword: document.getElementById("txb-password"),
  authContainer: document.getElementById("auth-container"),
  schoolPanel: document.getElementById("school-panel"),
  toast: document.getElementById("toast"),
  nameId: document.getElementById("email"),
  role: document.getElementById("role"),
  logout: document.getElementById("logout"),
  actionPanel: document.getElementById("action-panel"),
  panel: document.getElementById("overlay"),
  dataTable: document.getElementById("data"),
  spinner: document.getElementById("spinner"),
  cards: document.getElementById("cards"),
};
const methods = { Post: "POST", Get: "GET", Put: "PUT", Del: "DELETE" };
const Api = `http://localhost:5017/api`;
let CurrentUser = null;
let data = [];

UI.authTabs.addEventListener("click", (e) => {
  const btn = e.target.closest("button");
  if (!btn) return;
  [...UI.authTabs.querySelectorAll("button")].forEach((b) =>
    b.classList.remove("active"),
  );
  btn.classList.add("active");
  const type = btn.dataset.filter;
  UI.formTitle.textContent =
    UI.formBtn.textContent =
    UI.formBtn.dataset.action =
      type;
});

function showSpinner(show = true) {
  UI.spinner.classList.toggle("nDisplay", true);
}

function GetUserInfo() {
  const token = Token.getToken();
  const payload = JSON.parse(atob(token.split(".")[1]));
  const role =
    payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  const email =
    payload[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
    ];
  return { email: email, role: role, exp: payload.exp };
}

const Token = {
  saveToken(token) {
    document.cookie = `token=${token}; path=/; max-age:1800; Secure; SameSite=Strict`;
  },
  getToken() {
    return document.cookie
      .split("; ")
      .find((row) => row.startsWith("token" + "="))
      ?.split("=")[1];
  },
  removeToken() {
    document.cookie = "token=; max-age=0; path=/";
  },
  HasToken() {
    const token = this.getToken();
    return token || token.trim() !== "";
  },
  isTokenExp() {
    if (!this.HasToken()) return;
    console.log("Has token");
    CurrentUser = GetUserInfo();
    const def = CurrentUser.exp * 1000 - Date.now();
    return def <= 0;
  },
};
async function RequestApi(c, method = methods.Get, body) {
  let option = {
    method: method,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${Token.getToken()}`,
    },
  };
  if (body) {
    option.body = JSON.stringify(body);
  }
  try {
    const res = await fetch(`${Api}/${c}`, option);
    if (!res.ok) {
      throw new Error(`Request failed with status ${res.status}`);
    }
    return await res.json();
  } catch (e) {
    showToast("Error Happend!");
    console.error("Fetch failed:", e);
  }
}
function showToast(message) {
  UI.toast.textContent = message;
  UI.toast.style.display = "block";
  setTimeout(() => {
    UI.toast.style.display = "none";
  }, 2200);
}
function validateFrom() {
  const email = UI.txbEmail.value.trim();
  const password = UI.txbPassword.value.trim();
  if (!email) return { ok: false, message: "email is required" };
  if (password.length < 8)
    return { ok: false, message: "Password must be greather than 8" };
  return { ok: true, data: { email: email, password: password } };
}
async function handleLogin() {
  const result = validateFrom();
  if (!result.ok) {
    showToast(result.message);
    return;
  }
  const data = await RequestApi("Auth/Login", methods.Post, result.data);
  Token.saveToken(data.value.accessKey);
}
async function handleRegister() {
  const result = validateFrom();
  if (!result.ok) {
    showToast(result.message);
    return;
  }
  const data = await RequestApi("Auth/Register", methods.Post, result.data);
}
UI.formBtn.addEventListener("click", async (e) => {
  const btn = e.target.dataset.action;
  if (btn === "Login") await handleLogin();
  if (btn === "Register") await handleRegister();
  render();

  UI.txbEmail.value = "";
  UI.txbPassword.value = "";
});

function logout() {
  Token.removeToken();
  UI.authContainer.classList.remove("nDisplay");
  UI.schoolPanel.classList.add("nDisplay");
}

UI.logout.addEventListener("click", logout);

UI.actionPanel.addEventListener("click", async (e) => {
  const btn = e.target.closest("button");
  if (!btn) return;
  [...UI.actionPanel.querySelectorAll("button")].forEach((ele) =>
    ele.classList.remove("active"),
  );
  btn.classList.add("active");
});

UI.panel.addEventListener("click", (e) => {
  const close = e.target.closest(".cancel");
  if (!close) return;
  UI.panel.style.display = "none";
});

// Store global lists for dropdowns
// Store global lists for dropdowns
let _students = [],
  _subjects = [],
  _classes = [],
  _departments = [],
  _teachers = [];

async function renderPanel() {
  const icons = {
    Student: "🎓",
    Teacher: "👨‍🏫",
    Class: "🏫",
    Subject: "📚",
    Grade: "📝",
    Departement: "🏢",
    MyProfile: "👤",
    MyGrades: "📊",
    MyGrades: "📊",
    PassedStudents: "🏆",
    TeacherSubject: "🔗",
  };

  const createBtn = (api, label) => `
    <button data-api="${api}" type="button">
        <span class="nav-icon">${icons[api] || "📄"}</span>
        ${label}
    </button>
  `;

  let navContent = "";

  // Common link for everyone
  const passedStudentsLink = createBtn("PassedStudents", "Successful Students");

  if (CurrentUser.role === "Admin") {
    navContent = `
        ${createBtn("Student", "Students")}
        ${createBtn("Teacher", "Teachers")}
        ${createBtn("Class", "Classes")}
        ${createBtn("Subject", "Subjects")}
        ${createBtn("Grade", "Grades")}
        ${createBtn("Departement", "Departments")}
        ${createBtn("TeacherSubject", "Assign Teachers")}
        ${passedStudentsLink}
    `;
  } else if (CurrentUser.role === "User" || CurrentUser.role === "Student") {
    if (CurrentUser.role === "Student") {
      navContent = `
            ${createBtn("MyProfile", "My Profile")}
            ${createBtn("MyGrades", "My Grades")}
            ${createBtn("Departement", "Departments")}
            ${passedStudentsLink}
        `;
    } else {
      navContent = `
            ${createBtn("Student", "Students")}
            ${passedStudentsLink}
        `;
    }
  } else if (CurrentUser.role === "Teacher") {
    navContent = `
        ${createBtn("Student", "Students")}
        ${createBtn("Departement", "Departments")}
        ${createBtn("Grade", "Grades")}
        ${passedStudentsLink}
    `;
  }

  UI.actionPanel.innerHTML = navContent;

  [...UI.actionPanel.querySelectorAll("button")].forEach((ele) => {
    ele.addEventListener("click", async (e) => {
      [...UI.actionPanel.querySelectorAll("button")].forEach((b) =>
        b.classList.remove("active"),
      );
      const btn = e.target.closest("button");
      btn.classList.add("active");

      const t = btn.dataset.api;
      console.log("Navigating to:", t);
      document.getElementById("page-title").textContent =
        btn.textContent.trim();
      await loadData(t);
    });
  });

  await updateStats();
}

async function updateStats() {
  try {
    // Ensure we have data
    if (!_students.length) _students = await loadListForStats("Student");
    if (!_teachers.length) _teachers = await loadListForStats("Teacher");
    if (!_classes.length) _classes = await loadListForStats("Class");

    document.getElementById("stat-students").textContent = _students.length;
    document.getElementById("stat-teachers").textContent = _teachers.length;
    document.getElementById("stat-classes").textContent = _classes.length;
  } catch (e) {
    console.error("Stats error", e);
  }
}

// Helper for stats only (avoids clearing main table)
async function loadListForStats(entity) {
  const config = API_CONFIG[entity];
  const route = config && config.getAll ? config.getAll : entity;
  try {
    const res = await RequestApi(route);
    return Array.isArray(res) ? res : res && res.value ? res.value : [];
  } catch {
    return [];
  }
}

// --- API CONFIGURATION & MAPPING ---
// Maps strictly to C# Controllers
const API_CONFIG = {
  Student: {
    getAll: "Student/GetAllStudents",
    getById: "Student/GetStudentById",
    add: "Student/AddNewStudent",
    update: "Student/UpdateStudent",
    delete: "Student/DeleteStudent",
    idParam: "StudentId",
    deleteParam: "Id",
    getPassed: "Student/GetPassedStudents",
  },
  Teacher: {
    getAll: "Teacher/GetAllTeachers",
    getById: "Teacher/GetTeacherById",
    add: "Teacher/AddNewTeacher",
    update: "Teacher/UpdateTeacher",
    delete: "Teacher/DeleteTeacher",
    idParam: "TeacherId",
    deleteParam: "Id",
  },
  Grade: {
    getAll: "Grade/GetAllGrades",
    getById: "Grade/GetGradeById",
    add: "Grade/AddNewGrade",
    update: "Grade/UpdateGrade",
    delete: "Grade/DeleteGrade",
    idParam: "GradeId",
    deleteParam: "Id",
  },
  Class: {
    getAll: "Class/GetAllClasses",
    getById: "Class/GetClassById",
    add: "Class/AddNewClass",
    update: "Class/UpdateClass",
    delete: "Class/DeleteClass",
    idParam: "ClassId",
    deleteParam: "Id",
  },
  Subject: {
    getAll: "Subject/GetAllSubjects",
    getById: "Subject/GetSubjectById",
    add: "Subject/AddNewSubject",
    update: "Subject/UpdateSubject",
    delete: "Subject/DeleteSubject",
    idParam: "SubjectId",
    deleteParam: "Id",
  },
  Departement: {
    getAll: "Departement/GetAllDepartements",
    getById: "Departement/GetDepartementById",
    add: "Departement/AddNewDepartement",
    update: "Departement/UpdateDepartement",
    delete: "Departement/DeleteDepartement",
    idParam: "DepartementId",
    deleteParam: "Id",
  },
  TeacherSubject: {
    getAll: "TeacherSubject/GetAllTeacherSubjects",
    getById: "TeacherSubject/GetTeacherSubjectById",
    add: "TeacherSubject/AddNewTeacherSubject",
    update: "TeacherSubject/UpdateTeacherSubject",
    delete: "TeacherSubject/DeleteTeacherSubject",
    idParam: "Id", // Controller uses generic ID or query
    deleteParam: "Id",
  },
};

async function loadData(entity) {
  const tableContainer = document.getElementById("data");
  tableContainer.innerHTML = '<div class="spinner"></div>';

  try {
    let displayData = [];

    // Helper to get route
    const getRoute = (e) =>
      API_CONFIG[e] && API_CONFIG[e].getAll ? API_CONFIG[e].getAll : e;

    // Helper to unwrap response: { value: [...], isSuccess: true } -> [...]
    const fetchList = async (route) => {
      try {
        const res = await RequestApi(route);
        // If it's the specific wrapper Result<List<T>>, use .value
        // If it's a direct array (some APIs might), use it.
        return Array.isArray(res) ? res : res && res.value ? res.value : [];
      } catch (err) {
        // Backend returns 404 for empty lists (Result.Failure), treat as empty array
        if (err.message && err.message.includes("404")) {
          return [];
        }
        throw err;
      }
    };

    // Pre-fetch commonly used data
    try {
      if (!_students.length) _students = await fetchList(getRoute("Student"));
      if (!_subjects.length) _subjects = await fetchList(getRoute("Subject"));
      if (!_classes.length) _classes = await fetchList(getRoute("Class"));
      if (!_departments.length)
        _departments = await fetchList(getRoute("Departement"));
      if (!_teachers.length) _teachers = await fetchList(getRoute("Teacher"));
    } catch (e) {
      console.warn("Failed dependencies", e);
    }

    // --- PASSED STUDENTS VIEW ---
    if (entity === "PassedStudents") {
      const raw = await fetchList(API_CONFIG.Student.getPassed);
      displayData = raw.map((p) => ({
        Student: p.fullname || "Unknown",
        Class: p.classname || "Unknown",
        Grade: (p.grade || 0).toFixed(1) + "%", // Backend sends 'grade'
        Status: "PASSED",
      }));

      renderTable(displayData, entity); // Use standard table or custom
      return;
    }

    // --- STUDENT VIEW LOGIC ---
    if (entity === "MyProfile") {
      const allStudents = _students;
      const myProfile = allStudents.find((s) => s.email === CurrentUser.email);
      if (!myProfile) {
        tableContainer.innerHTML =
          '<p class="text-center">Profile not found for this account.</p>';
        return;
      }
      renderProfileCard(myProfile);
      return;
    }

    if (entity === "MyGrades") {
      const allGrades = await fetchList(getRoute("Grade"));
      const myInfo = _students.find((s) => s.email === CurrentUser.email);

      if (myInfo) {
        displayData = allGrades.filter((g) => g.studentId === myInfo.id);
      }
      if (displayData.length) {
        displayData = displayData.map((g) => ({
          Subject: _subjects.find((s) => s.id === g.subjectId)?.name || "N/A",
          Value: g.value || g.score || 0,
          Date: (g.date || g.createAt || "").split("T")[0],
        }));
      }
    }

    // --- TEACHER SUBJECT SPECIAL LOGIC (Map IDs to Names) ---
    else if (entity === "TeacherSubject") {
      const rawData = await fetchList(getRoute(entity));
      displayData = rawData.map((item) => ({
        id: item.id,
        Teacher:
          _teachers.find((t) => t.id === item.teacherId)?.fullName ||
          `ID:${item.teacherId}`,
        Subject:
          _subjects.find((s) => s.id === item.subjectId)?.subjectName ||
          `ID:${item.subjectId}`,
        Class:
          _classes.find((c) => c.id === item.classId)?.className ||
          `ID:${item.classId}`,
        // Keep IDs hidden for edit but useful if needed, here we just show names
      }));
    }

    // --- TEACHER/ADMIN LOGIC ---
    else {
      const raw = await fetchList(getRoute(entity));

      if (entity === "Student") {
        displayData = raw.map((s) => ({
          id: s.id,
          FullName: s.fullName,
          Class:
            _classes.find((c) => c.id === s.classId)?.className || s.classId,
          Gender: s.gender === 0 ? "Male" : "Female",
          DateOfBirth: (s.dateOfBirth || "").split("T")[0],
          EnrollmentDate: (s.enrollmentDate || "").split("T")[0],
        }));
      } else if (entity === "Teacher") {
        displayData = raw.map((t) => ({
          id: t.id,
          FullName: t.fullName,
          Department:
            _departments.find((d) => d.id === t.departmentId)?.name ||
            t.departmentId,
          HireDate: (t.hireDate || "").split("T")[0],
        }));
      } else if (entity === "Grade") {
        displayData = raw.map((g) => ({
          id: g.id,
          Student:
            _students.find((s) => s.id === g.studentId)?.fullName ||
            g.studentId,
          Subject:
            _subjects.find((s) => s.id === g.subjectId)?.subjectName ||
            g.subjectId,
          Score: g.value || g.score,
          Date: (g.createAt || g.date || "").split("T")[0],
        }));
      } else if (entity === "Class") {
        displayData = raw.map((c) => ({
          id: c.id,
          ClassName: c.className || c.name,
          Year: (c.year || "").split("-")[0],
        }));
      } else if (entity === "Subject") {
        displayData = raw.map((s) => ({
          id: s.id,
          SubjectName: s.subjectName || s.name,
        }));
      } else {
        displayData = raw;
      }
    }

    if (!displayData || displayData.length === 0) {
      tableContainer.innerHTML = `
                <div style="text-align:center; padding:40px;">
                    <p style="opacity:0.7; margin-bottom:15px; font-size:1.1rem;">No ${entity} data available.</p>
                    ${
                      (CurrentUser.role === "Teacher" && entity === "Grade") ||
                      CurrentUser.role === "Admin"
                        ? `<button class="btn-primary" onclick="openModal('${entity}')">+ Add New ${entity}</button>`
                        : ""
                    }
                </div>`;
      return;
    }

    renderTable(displayData, entity);
  } catch (error) {
    tableContainer.innerHTML =
      '<p style="text-align:center; color:var(--accent-danger);">Failed to load data.</p>';
    console.error(error);
  }
}

function renderPassedStudentsView(students, grades, classes) {
  const container = document.getElementById("data");

  // Calculate Averages
  const passedStudents = students
    .map((student) => {
      const studentGrades = grades.filter((g) => g.studentId === student.id);
      if (!studentGrades.length) return null;

      const sum = studentGrades.reduce((a, b) => a + b.value, 0);
      const avg = sum / studentGrades.length;

      if (avg >= 50) {
        // Pass Mark
        return {
          id: student.id,
          name: `${student.firstName} ${student.lastName}`,
          classId: student.classId,
          className:
            classes.find((c) => c.id === student.classId)?.name || "Unknown",
          average: avg.toFixed(1),
        };
      }
      return null; // Fail
    })
    .filter((s) => s !== null);

  // Render Filters
  const classOptions = classes
    .map((c) => `<option value="${c.id}">${c.className || c.name}</option>`)
    .join("");

  container.innerHTML = `
        <div class="filter-bar">
             <label>✨ Filter by Class:</label>
             <select id="filter-class">
                <option value="all">All Classes</option>
                ${classOptions}
             </select>
        </div>
        <div id="passed-table-container"></div>
    `;

  const renderFilteredTable = (filterClassId) => {
    let displayList = passedStudents;
    if (filterClassId !== "all") {
      displayList = passedStudents.filter((s) => s.classId == filterClassId);
    }

    if (displayList.length === 0) {
      document.getElementById("passed-table-container").innerHTML = `
                <div style="text-align:center; padding:40px; color:var(--text-secondary);">
                    <p style="font-size:1.1rem;">No passed students found regarding this criteria.</p>
                </div>`;
      return;
    }

    let html = `
          <table>
            <thead>
                <tr>
                    <th>Student Name</th>
                    <th>Class</th>
                    <th>Average Grade</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
        `;
    html += displayList
      .map(
        (row) => `
            <tr>
                <td>${row.name}</td>
                <td>${row.className}</td>
                <td style="color:var(--accent-teal); font-weight:bold;">${row.average}%</td>
                <td><span class="pill" style="background:rgba(34, 197, 94, 0.2); color:#22c55e;">PASSED</span></td>
            </tr>
        `,
      )
      .join("");
    html += `</tbody></table>`;
    document.getElementById("passed-table-container").innerHTML = html;
  };

  // Initial Render
  renderFilteredTable("all");

  // Event Listener
  document.getElementById("filter-class").addEventListener("change", (e) => {
    renderFilteredTable(e.target.value);
  });
}

function renderProfileCard(profile) {
  const tableContainer = document.getElementById("data");
  tableContainer.innerHTML = `
        <div class="profile-view">
            <div class="profile-card">
                <div class="profile-avatar">
                   ${profile.firstName.charAt(0)}${profile.lastName.charAt(0)}
                </div>
                <div class="profile-details">
                    <h1>My Profile</h1>
                    <div class="detail-row">
                        <div class="detail-label">Name:</div>
                        <div class="detail-value">${profile.firstName} ${profile.lastName}</div>
                    </div>
                     <div class="detail-row">
                        <div class="detail-label">Email:</div>
                         <div class="detail-value">${profile.email}</div>
                    </div>
                     <div class="detail-row">
                        <div class="detail-label">Phone:</div>
                         <div class="detail-value">${profile.phone || "Not Set"}</div>
                    </div>
                     <div class="detail-row">
                        <div class="detail-label">Address:</div>
                         <div class="detail-value">${profile.address || "Not Set"}</div>
                    </div>
                </div>
            </div>
        </div>
    `;
}

function renderTable(data, entity) {
  const tableContainer = document.getElementById("data");

  // Add Button for Teacher/Admin
  let headerAction = "";
  if (
    (CurrentUser.role === "Teacher" && entity === "Grade") ||
    CurrentUser.role === "Admin"
  ) {
    headerAction = `
            <div style="display:flex; justify-content:space-between; margin-bottom:15px; width:100%; flex-wrap:wrap; gap:10px;">
                <input type="text" id="table-search" placeholder="🔍 Search..." style="padding:10px; border-radius:10px; border:1px solid var(--glass-border); background:rgba(255,255,255,0.05); color:white; flex:1; min-width:200px;">
                <button class="btn-primary" onclick="openModal('${entity}')">
                    <span>+</span> Add New ${entity}
                </button>
            </div>
        `;
  } else {
    headerAction = `
            <div style="display:flex; justify-content:flex-start; margin-bottom:15px; width:100%;">
                <input type="text" id="table-search" placeholder="🔍 Search..." style="padding:10px; border-radius:10px; border:1px solid var(--glass-border); background:rgba(255,255,255,0.05); color:white; flex:1; max-width:400px;">
            </div>
        `;
  }

  const renderBody = (filteredData) => {
    const tbody = tableContainer.querySelector("tbody");
    if (!tbody) return;

    tbody.innerHTML = filteredData
      .map(
        (row) => `
        <tr>
            ${headers.map((key) => `<td>${row[key]}</td>`).join("")}
            ${
              CurrentUser.role === "Admin"
                ? `
            <td style="display:flex; gap:10px;">
                <button class="btn-secondary" onclick="openModal('${entity}', '${row.id}')">✏️ Edit</button>
                <button class="danger" onclick="deleteItem('${entity}', '${row.id}')">🗑️ Delete</button>
            </td>`
                : ""
            } 
        </tr>
    `,
      )
      .join("");
  };

  const headers = Object.keys(data[0]).filter(
    (h) => h.toLowerCase() !== "userid" && h.toLowerCase() !== "id",
  );

  let html = `
      ${headerAction}
      <div style="overflow-x:auto;">
      <table>
        <thead>
            <tr>
                ${headers.map((h) => `<th>${h}</th>`).join("")}
                ${CurrentUser.role === "Admin" ? "<th>Actions</th>" : ""} 
            </tr>
        </thead>
        <tbody>
    `;

  html += `</tbody></table></div>`;
  tableContainer.innerHTML = html;

  // Render initial rows
  renderBody(data);

  // Attach Search Listener
  const searchInput = document.getElementById("table-search");
  if (searchInput) {
    searchInput.addEventListener("keyup", (e) => {
      const term = e.target.value.toLowerCase();
      const filtered = data.filter((row) =>
        Object.values(row).some((val) =>
          String(val).toLowerCase().includes(term),
        ),
      );
      renderBody(filtered);
    });
  }

  // Add animation
  tableContainer.classList.remove("fade-in");
  void tableContainer.offsetWidth; // trigger reflow
  tableContainer.classList.add("fade-in");
}

// --- UNIFIED MODAL LOGIC (ADD & EDIT) ---
window.openModal = async (entity, id = null) => {
  const modal = document.getElementById("overlay");
  const modalBody = document.getElementById("modal-body");
  const confirmBtn = document.getElementById("confirm");
  const title = document.getElementById("modal-title");

  const isEdit = id !== null;
  title.textContent = isEdit ? `Edit ${entity}` : `Add New ${entity}`;

  modal.classList.remove("nDisplay");
  modal.style.display = "flex";

  let existingData = null;
  if (isEdit) {
    modalBody.innerHTML = '<div class="spinner"></div>';
    try {
      // Construct GET URL with Param
      const config = API_CONFIG[entity];
      if (config && config.getById) {
        const paramName = config.idParam || "Id";
        const url = `${config.getById}?${paramName}=${id}`;
        const response = await RequestApi(url);
        existingData = response.value || response;
      } else {
        existingData = await RequestApi(`${entity}/${id}`);
      }
    } catch (e) {
      console.error("Failed to fetch item", e);
      if (entity === "Student")
        existingData = _students.find((x) => x.id == id);
      else if (entity === "Class")
        existingData = _classes.find((x) => x.id == id);
    }
  }

  // --- DTO HELPERS ---
  const val = (k) =>
    existingData && existingData[k] !== undefined ? existingData[k] : "";
  const dateVal = (k) => {
    if (!existingData || !existingData[k]) return "";
    return existingData[k].split("T")[0];
  };

  let formHtml = "";

  // --- GRADE FORM ---
  if (entity === "Grade") {
    // Backend DTO: Score, CreateAt
    const currentScore = val("score") || val("value");

    const studentOpts = _students
      .map(
        (s) =>
          `<option value="${s.id}" ${val("studentId") == s.id ? "selected" : ""}>${s.firstName && s.lastName ? s.firstName + " " + s.lastName : s.fullName || s.name}</option>`,
      )
      .join("");
    const subjectOpts = _subjects
      .map(
        (s) =>
          `<option value="${s.id}" ${val("subjectId") == s.id ? "selected" : ""}>${s.subjectName || s.name}</option>`,
      )
      .join("");

    formHtml = `
            <label>Student</label>
            <input type="text" id="grade-student-search" class="modal-search-input" placeholder="🔍 Type to search student...">
            <select id="inp-studentId" class="dropdown">${studentOpts}</select>
            
            <label>Subject</label>
            <select id="inp-subjectId" class="dropdown">${subjectOpts}</select>
            
            <label>Score</label>
            <input type="number" id="inp-score" placeholder="e.g. 85" value="${currentScore || ""}">
            
            <label>Date</label>
            <input type="date" id="inp-date" value="${dateVal("createAt") || dateVal("date")}">
        `;
  }
  // --- TEACHER FORM ---
  else if (entity === "Teacher") {
    const deptOpts = _departments
      .map(
        (s) =>
          `<option value="${s.id}" ${val("departmentId") == s.id ? "selected" : ""}>${s.name}</option>`,
      )
      .join("");

    formHtml = `
        <label>Full Name</label>
        <input type="text" id="inp-fullname" value="${val("fullName")}" placeholder="Teacher Name">
        
        <label>User ID</label>
        <input type="text" id="inp-userid" value="${val("userId") || ""}">

        <label>Department</label>
        <select id="inp-departmentId" class="dropdown">${deptOpts}</select>

        <label>Hire Date</label>
        <input type="date" id="inp-hiredate" value="${dateVal("hireDate")}">
     `;
  }
  // --- SUBJECT FORM ---
  else if (entity === "Subject") {
    formHtml = `
        <label>Subject Name</label>
        <input type="text" id="inp-subjectName" value="${val("subjectName") || val("name")}" placeholder="e.g. Mathematics">
    `;
  }
  // --- CLASS FORM ---
  else if (entity === "Class") {
    formHtml = `
        <label>Class Name</label>
        <input type="text" id="inp-className" value="${val("className") || val("name")}" placeholder="e.g. Grade 10 A">
        
        <label>Year</label>
        <input type="number" id="inp-year" value="${val("year") ? val("year").split("-")[0] : new Date().getFullYear()}" placeholder="YYYY">
    `;
  }
  // --- TEACHER SUBJECT FORM ---
  else if (entity === "TeacherSubject") {
    const teacherOpts = _teachers
      .map(
        (t) =>
          `<option value="${t.id}" ${val("teacherId") == t.id ? "selected" : ""}>${t.fullName}</option>`,
      )
      .join("");
    const subjectOpts = _subjects
      .map(
        (s) =>
          `<option value="${s.id}" ${val("subjectId") == s.id ? "selected" : ""}>${s.subjectName || s.name}</option>`,
      )
      .join("");
    const classOpts = _classes
      .map(
        (c) =>
          `<option value="${c.id}" ${val("classId") == c.id ? "selected" : ""}>${c.className || c.name}</option>`,
      )
      .join("");

    formHtml = `
        <label>Teacher</label>
        <select id="inp-teacherId" class="dropdown">${teacherOpts}</select>
        
        <label>Subject</label>
        <select id="inp-subjectId" class="dropdown">${subjectOpts}</select>
        
        <label>Class</label>
        <select id="inp-classId" class="dropdown">${classOpts}</select>
    `;
  }
  // --- STUDENT FORM ---
  else if (entity === "Student") {
    // Backend DTO: FullName, DateOfBirth, EnrollmentDate, Gender, ClassId
    const classOpts = _classes
      .map(
        (s) =>
          `<option value="${s.id}" ${val("classId") == s.id ? "selected" : ""}>${s.className || s.name}</option>`,
      )
      .join("");
    const fullName =
      val("fullName") ||
      (val("firstName") ? val("firstName") + " " + val("lastName") : "").trim();

    formHtml = `
        <label>Full Name</label>
        <input type="text" id="inp-fullname" value="${fullName}" placeholder="Full Name">
        
        <label>Class</label>
        <select id="inp-classId" class="dropdown">${classOpts}</select>

        <label>Date of Birth</label>
        <input type="date" id="inp-dob" value="${dateVal("dateOfBirth")}">
        
        <label>Enrollment Date</label>
        <input type="date" id="inp-enroll" value="${dateVal("enrollmentDate")}">
        
        <label>Gender</label>
        <select id="inp-gender" class="dropdown">
            <option value="0" ${val("gender") === 0 ? "selected" : ""}>Male</option>
            <option value="1" ${val("gender") === 1 ? "selected" : ""}>Female</option>
        </select>
        <p style="font-size:0.8rem; color:var(--text-muted); margin-top:5px;">* Email/Phone are managed by User Account.</p>
      `;
  } else {
    // Generic
    formHtml = `
            <label>Name</label>
            <input type="text" id="inp-name" value="${val("name")}">
        `;
  }

  modalBody.innerHTML = formHtml;

  // --- Search Filter Logic for Grade ---
  if (entity === "Grade") {
    const searchInp = document.getElementById("grade-student-search");
    const studentSelect = document.getElementById("inp-studentId");
    if (searchInp && studentSelect) {
      const originalOptions = Array.from(studentSelect.options); // Keep backup

      searchInp.addEventListener("input", (e) => {
        const filter = e.target.value.toLowerCase();

        // Filter options data
        const filteredOpts = originalOptions.filter((opt) =>
          opt.text.toLowerCase().includes(filter),
        );

        // Rebuild select HTML
        studentSelect.innerHTML = filteredOpts
          .map((opt) => opt.outerHTML)
          .join("");

        // Select first if available
        if (filteredOpts.length > 0) {
          studentSelect.value = filteredOpts[0].value;
        }
      });
      // Focus on search input automatically
      setTimeout(() => searchInp.focus(), 100);
    }
  }

  confirmBtn.onclick = async () => {
    const body = {};

    if (entity === "Grade") {
      body.studentId = parseInt(document.getElementById("inp-studentId").value);
      body.subjectId = parseInt(document.getElementById("inp-subjectId").value);
      body.score = parseFloat(document.getElementById("inp-score").value);
      body.createAt =
        document.getElementById("inp-date").value || new Date().toISOString();
      // Ensure backend optional fields aren't null if required
    } else if (entity === "Student") {
      body.fullName = document.getElementById("inp-fullname").value;
      body.classId = parseInt(document.getElementById("inp-classId").value);
      body.dateOfBirth =
        document.getElementById("inp-dob").value || new Date().toISOString();
      body.enrollmentDate =
        document.getElementById("inp-enroll").value || new Date().toISOString();
      body.gender = parseInt(document.getElementById("inp-gender").value);
      // UserId is required for Add, assuming empty string or handled by backend?
      if (!isEdit) body.userId = "temp-user-id"; // Placeholder if required
    } else if (entity === "Teacher") {
      body.fullName = document.getElementById("inp-fullname").value;
      body.userId = document.getElementById("inp-userid").value;
      body.departmentId = parseInt(
        document.getElementById("inp-departmentId").value,
      );
      body.hireDate =
        document.getElementById("inp-hiredate").value ||
        new Date().toISOString();
    } else if (entity === "Class") {
      body.className = document.getElementById("inp-className").value;
      const y = document.getElementById("inp-year").value;
      body.year = y ? `${y}-01-01` : new Date().toISOString().split("T")[0];
    } else if (entity === "TeacherSubject") {
      body.teacherId = parseInt(document.getElementById("inp-teacherId").value);
      body.subjectId = parseInt(document.getElementById("inp-subjectId").value);
      body.classId = parseInt(document.getElementById("inp-classId").value);
    } else if (entity === "Subject") {
      body.subjectName = document.getElementById("inp-subjectName").value;
    } else {
      body.name = document.getElementById("inp-name")?.value;
    }

    if (isEdit) body.id = id;

    const method = isEdit ? methods.Put : methods.Post;

    // Resolve URL
    let url = entity;
    const config = API_CONFIG[entity];
    if (config) {
      url = isEdit ? config.update : config.add;
    }

    try {
      await RequestApi(url, method, body);
      showToast(isEdit ? "Updated Successfully!" : "Added Successfully!");
      modal.style.display = "none";
      await loadData(entity);
    } catch (err) {
      showToast("Operation Failed");
      console.error(err);
    }
  };
};

// Close modal event
document.getElementById("cancel").addEventListener("click", () => {
  document.getElementById("overlay").style.display = "none";
});

// Global for inline onclick
window.deleteItem = async (entity, id) => {
  if (confirm("Are you sure?")) {
    let url = `${entity}/${id}`;
    const config = API_CONFIG[entity];
    if (config && config.delete) {
      const param = config.deleteParam || "Id";
      url = `${config.delete}?${param}=${id}`;
    }

    await RequestApi(url, methods.Del);
    showToast("Deleted Successfully");
    await loadData(entity);
  }
};

function render() {
  if (Token.isTokenExp()) {
    logout();
  }
  UI.authContainer.classList.add("nDisplay");
  UI.schoolPanel.classList.remove("nDisplay"); // Grid layout

  UI.nameId.textContent = CurrentUser.email;
  UI.role.textContent = CurrentUser.role;
  renderPanel();

  if (data.length === 0) return;
}

(function boot() {
  if (Token.HasToken()) {
    CurrentUser = GetUserInfo(); // Restore user if token exists
    render();
  }
})();
