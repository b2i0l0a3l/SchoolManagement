export default function ExamsSection() {
    return (
        <div className="glass-panel rounded-2xl p-6 shadow-sm border border-gray-100">
                    <h2 className="text-lg font-bold text-gray-900 mb-4">الامتحانات القادمة</h2>
                    <div className="space-y-4">
                        {['الرياضيات', 'الفيزياء', 'اللغة الإنجليزية'].map((subject, index) => (
                            <div key={subject} className="flex items-center justify-between p-3 rounded-xl border border-gray-100">
                                <div className="flex items-center gap-3">
                                    <div className="h-2 w-2 rounded-full bg-accent"></div>
                                    <span className="font-medium text-gray-900">{subject}</span>
                                </div>
                                <span className="text-sm text-gray-500">غداً، 09:00 ص</span>
                            </div>
                        ))}
                    </div>
                </div>
    )
}