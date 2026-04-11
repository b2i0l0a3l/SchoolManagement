export default function AuthLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <div style={{ width: '100vw', minHeight: '100vh' }}>
      {children}
    </div>
  );
}
