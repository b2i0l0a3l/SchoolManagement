export default function Notification() {
    return  <div className="grid grid-cols-1 gap-6 lg:grid-cols-2 mt-8 animate-slide-up" style={{ animationDelay: '400ms' }}>
                <div className="glass-panel rounded-2xl p-6 shadow-sm border border-gray-100">
                    <h2 className="text-lg font-bold text-gray-900 mb-4">أحدث الإشعارات</h2>
                    <div className="space-y-4">
                        {[1, 2, 3].map((i) => (
                            <div key={i} className="flex gap-4 p-3 rounded-xl hover:bg-gray-50 transition-colors cursor-pointer border border-transparent hover:border-gray-100">
                                <div className="h-10 w-10 rounded-full bg-blue-100 flex items-center justify-center shrink-0">
                                    <span className="text-blue-600 font-bold">{i}</span>
                                </div>
                                <div>
                                    <p className="text-sm font-semibold text-gray-900">تم تسجيل طالب جديد</p>
                                    <p className="text-xs text-gray-500 mt-1">قبل {i} ساعة</p>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
    </div>
    
}