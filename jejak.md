# Jejak Perubahan Proyek Parking System

## Tanggal: 22 Maret 2025

### ParkingOut (Aplikasi Klien)

1. **Perbaikan Koneksi Database**
   - Ditambahkan metode `IsPostgreSqlInstalled()` untuk verifikasi instalasi PostgreSQL
   - Ditingkatkan penanganan error pada metode `InitializeConnectionString()`
   - Ditambahkan logging yang lebih detil untuk memudahkan debugging
   - Perbaikan pada pengecekan tabel schema untuk memastikan struktur database valid

2. **Penanganan Error**
   - Ditingkatkan pesan error yang lebih informatif pada `TestConnection()`
   - Ditambahkan pengecekan error pada pembuatan database
   - Implementasi try-catch yang lebih baik di berbagai metode koneksi database

3. **Package dan Dependensi**
   - Ditambahkan referensi package Npgsql untuk koneksi PostgreSQL
   - Ditambahkan referensi package ClosedXML untuk fungsionalitas laporan
   - Memastikan semua project memiliki dependensi yang tepat

4. **File dan Kode**
   - Diperbarui `BackupRestoreForm.cs` dan `LoginForm.cs` untuk menggunakan Npgsql
   - Diperbarui schema PostgreSQL untuk menangani pembuatan database dengan benar
   - Ditambahkan metode kompatibilitas untuk mendukung kode lama

### ParkingServer (Aplikasi Server)

1. **Perbaikan Koneksi Database**
   - Ditambahkan metode `IsPostgreSqlInstalled()` untuk verifikasi instalasi PostgreSQL
   - Ditambahkan metode `TestDatabaseConnection()` untuk pengujian koneksi
   - Ditambahkan metode `CreateParkirDatabase()` untuk pembuatan database otomatis
   - Implementasi indikator status database dengan property `IsDatabaseAvailable`

2. **WebSocket Server**
   - Ditambahkan metode `InitializeDatabase()` untuk inisialisasi koneksi database
   - Diperbarui alur kerja server untuk meneruskan operasi meskipun tanpa database
   - Ditingkatkan logging untuk memudahkan pemantauan server

3. **Penanganan Error**
   - Pesan error yang lebih informatif dan user-friendly
   - Server dapat beradaptasi saat database tidak tersedia dengan membatasi fungsionalitas
   - Penanganan kasus PostgreSQL tidak terinstal atau tidak berjalan

4. **Performa dan Stabilitas**
   - Ditingkatkan penanganan koneksi WebSocket untuk stabilitas lebih baik
   - Ditambahkan logging status koneksi dan jumlah client aktif
   - Implementasi pengelolaan siklus hidup server yang lebih baik

## Catatan Tambahan

- Kedua aplikasi sekarang dapat berjalan dengan PostgreSQL dan telah diuji pada sistem Windows
- Database parkirdb dibuat secara otomatis jika belum ada
- Schema database otomatis dimuat saat pembuatan database baru
- Semua koneksi database menggunakan kredensial default: username=postgres, password=root@rsi
- Server WebSocket berjalan pada port 8181 

## Tanggal: 27 Maret 2025

### Perbaikan Lanjutan ParkingOut (Aplikasi Klien)

1. **Peningkatan Ketahanan Koneksi Database**
   - Ditambahkan property `IsDatabaseAvailable` dan `LastError` di class `Database` untuk melacak status koneksi
   - Dimodifikasi static constructor pada `Database.cs` untuk menangkap exception tanpa menghentikan aplikasi
   - Implementasi mekanisme fallback agar aplikasi tetap bisa berjalan meskipun database tidak tersedia

2. **Perbaikan Form Login**
   - Diperbarui metode `TestDatabaseConnection()` untuk mengecek status koneksi secara lebih akurat
   - Ditambahkan UI yang lebih informatif saat database tidak tersedia (pesan error dan status)
   - Implementasi tombol login dengan validasi dan hashing password yang lebih baik
   - Ditingkatkan fitur animasi form untuk UX yang lebih baik

3. **Peningkatan Error Handling di Program Utama**
   - Dimodifikasi `Program.cs` untuk menangani exception dengan lebih baik
   - Ditambahkan pembuatan direktori log otomatis jika belum ada
   - Diubah alur startup aplikasi agar tetap bisa berjalan dengan fungsionalitas terbatas
   - Ditambahkan metode `ShowErrorMessage()` untuk menampilkan pesan error yang lebih informatif

4. **Keamanan dan Robustness**
   - Implementasi hashing password menggunakan SHA-256 di `LoginForm.cs`
   - Pemisahan logika login ke metode terpisah untuk maintainability yang lebih baik
   - Penanganan kondisi edge-case dan validasi input yang lebih ketat
   - Logging yang lebih komprehensif untuk memudahkan troubleshooting

### Masalah yang Berhasil Diselesaikan

1. **Form Kosong Saat Startup**
   - Aplikasi sekarang selalu menampilkan form login meskipun koneksi database gagal
   - Pesan error yang jelas ditampilkan kepada user saat koneksi database bermasalah
   - User bisa melihat status koneksi dan mendapat informasi akurat tentang masalah

2. **Crash Aplikasi Saat Database Tidak Tersedia**
   - Aplikasi tidak lagi crash saat PostgreSQL service tidak berjalan
   - Error handling yang lebih baik di semua lapisan aplikasi
   - Tombol exit selalu tersedia untuk menutup aplikasi dengan aman

## Catatan Teknis

- Untuk menjalankan aplikasi dengan baik, pastikan PostgreSQL service aktif (`postgresql-x64-14`)
- Kredensial default: username=postgres, password=root@rsi
- Database: parkirdb (dibuat otomatis jika belum ada)
- File log aplikasi tersedia di folder `Logs` di direktori aplikasi untuk troubleshooting lanjutan 