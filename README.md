# DoDAT

## Opis

ToDo Application to bardzo prosta aplikacja stworzona w technologii ASP.NET w modelu MVC, wykorzystująca .NET 8 oraz Entity Framework. Aplikacja umożliwia zarządzanie listą zadań, co pozwala użytkownikom na dodawanie, edytowanie oraz usuwanie zadań.

## Funkcjonalności

- Dodawanie nowych zadań
- Edytowanie istniejących zadań
- Usuwanie zadań
- Wyświetlanie listy wszystkich zadań
- Wyświetlanie listy zadań dla danego dnia

## Wymagania

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQLite
- EF

## Instalacja

1. **Pobierz repozytorium:**

   ```bash
   git clone https://github.com/JakubRoss/DoDAT

   ```

2. **Przejdź do katalogu aplikacji:**

   ```bash
   cd DoDAT

   ```

3. **Zainstaluj wymagane pakiety NuGet:**

   ```bash
   dotnet restore

   ```

4. **Skonfiguruj bazę danych:**

   ```bash
   "ConnectionStrings": {
    "DefaultConnection": "Data Source=ToDoDb.sqlite"}
   ```

W pliku appsettings.json skonfiguruj połączenie z bazą danych SQLite

5. **Zastosuj migracje:**

   ```bash
   dotnet ef database update

   ```

Wykonaj polecenie, aby utworzyć bazę danych

6. **Uruchom aplikację:**

   ```bash
   dotnet run
   Aplikacja będzie dostępna pod adresem http://localhost:5000
   ```

## Images

![This is an alt text.](/Zrzut%20ekranu%202024-10-30%20100236.png "This is a sample image.")

# OR Wejdz w link:

http://localhost:5000
