using System;
using System.Linq;
using System.Text;
using FamilyWallet.Core.Interfaces;
using FamilyWallet.Core.Services;
using FamilyWallet.Core.Validators;
using System.Globalization;

Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.UTF8;

CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("uk-UA");
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("uk-UA");

var data = new FamilyWalletData();
var transactionService = new TransactionService(data);
var analyticsService = new AnalyticsService(data.Transactions);
var budgetService = new BudgetService(data);
var goalService = new FinancialGoalService(data, transactionService);

IAuthService authService = new AuthService(data);
IFamilyManagementService familyService = new FamilyManagementService(data);

string currentUserId = string.Empty;
string currentFamilyId = string.Empty;

// МЕНЮ 1. АУТЕНТИФІКАЦІЯ ТА РЕЄСТРАЦІЯ

while (string.IsNullOrEmpty(currentUserId))
{
    Console.WriteLine(); 
    Console.WriteLine("Ласкаво просимо до програми FamilyWallet!"); 
    Console.WriteLine("Пройдіть автентифікацію:"); 
    Console.WriteLine("1. Увійти в програму"); 
    Console.WriteLine("2. Зареєструватися"); 
    Console.Write("Виберіть дію: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Вхід поки що не реалізований. Будь ласка, зареєструйтесь.");
            break;

        case "2":
            var validator = new ValidationService();
            string username;
            string email;
            string password;

            while (true)
            {
                Console.Write("Введіть ім'я користувача: ");
                username = Console.ReadLine() ?? string.Empty;

                if (validator.IsValidUsername(username))
                {
                    break;
                }
                Console.WriteLine("Помилка: введіть коректне ім'я користувача.");
            }

            while (true)
            {
                Console.Write("Введіть email: ");
                email = Console.ReadLine() ?? string.Empty;

                if (validator.IsValidEmail(email))
                {
                    break;
                }
                Console.WriteLine("Помилка: введіть коректний email.");
            }

            while (true)
            {
                Console.Write("Введіть пароль: ");
                password = Console.ReadLine() ?? string.Empty;

                if (validator.IsValidPassword(password))
                {
                    break;
                }
                Console.WriteLine("Помилка: пароль має містити щонайменше 8 символів, " + "велику літеру та цифру.");
            }

            bool registered = authService.Register(username, email, password);

            if (!registered)
            {
                Console.WriteLine("Реєстрація не виконано. Перевірте введені дані " + "або спробуйте інший email.");
                break;
            }

            var user = data.Users.FirstOrDefault(u =>string.Equals(u.Email,email.Trim(),StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                Console.WriteLine("Неможливо отримати дані користувача.");
                break;
            }

            currentUserId = user.Id;
            Console.WriteLine($"Реєстрація успішна! Ласкаво просимо, {user.Username}.");
            Console.WriteLine();
            Console.Write("Створити сімейну групу зараз? (так/ні): ");

            string? createFamily = Console.ReadLine();

            if (string.Equals(createFamily?.Trim(),"так",StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Введіть назву сімейної групи: ");
                string familyName = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(familyName))
                {
                    try
                    {
                        familyService.CreateFamilyGroup(familyName);
                        var family = data.FamilyGroups.LastOrDefault(f => f.Name == familyName.Trim());

                        if (family != null)
                        {
                            currentFamilyId = family.Id;
                            familyService.AddMember(currentFamilyId,currentUserId);
                            Console.WriteLine( $"Сімейна група \"{family.Name}\" створена.");
                            Console.WriteLine("Ви додані у сімейну групу.");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Назва групи не може бути порожньою.");
                }
            }
            else
            {
                Console.WriteLine("Реєстрацію не виконано. Виправте помилки.");
            }

            break;

        default:
            Console.WriteLine("Виберіть 1 або 2.");
            break;
    }
}

// ==================================================
// МЕНЮ 2. ОСНОВНІ ФУНКЦІЇ FAMILYWALLET
// ==================================================

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("=== FamilyWallet ===");
    Console.WriteLine("1. Дохід");
    Console.WriteLine("2. Витрата");
    Console.WriteLine("3. Баланс");
    Console.WriteLine("4. Статистика");
    Console.WriteLine("5. Встановити ліміт бюджету");
    Console.WriteLine("6. Показати наявні ліміти бюджету");
    Console.WriteLine("7. Створити фінансову ціль");
    Console.WriteLine("8. Поповнити фінансову ціль");
    Console.WriteLine("9. Показати наявні финансові цілі");
    Console.WriteLine("10. Створити сімейну групу");
    Console.WriteLine("11. Додати учасника у групу");
    Console.WriteLine("0. Вихід");
    Console.WriteLine();
    Console.Write("Оберіть дію: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
        case "2":
        {
            bool isIncome = choice == "1";

            Console.Write("Категорія: ");
            string category = Console.ReadLine() ?? string.Empty;

            Console.Write("Сума: ");

            if (!decimal.TryParse(
                Console.ReadLine(), out decimal amount) ||
                amount <= 0 ||
                string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine(
                    "Введіть коректну категорію та позитивну суму.");
                break;
            }

            try
            {
                transactionService.AddTransaction(
                    category, amount, isIncome);

                Console.WriteLine("Транзакція успішно додана.");

                if (!isIncome)
                {
                    var limit = data.BudgetLimits.FirstOrDefault(b =>
                        string.Equals(
                            b.CategoryId,
                            category.Trim(),
                            StringComparison.OrdinalIgnoreCase));

                    if (limit != null && budgetService.IsLimitExceeded(
                        category.Trim()))
                    {
                        Console.WriteLine(
                            $"Увага! Перевищено ліміт категорії " +
                            $"\"{category}\" ({limit.MaxAmount:N2}).");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            break;
        }

        case "3":
            Console.WriteLine(
                $"Поточний баланс: {transactionService.GetTotalBalance():N2}");
            break;

        case "4":
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            Console.WriteLine("=== Статистика за сьогодні ===");

            Console.WriteLine(
                $"Доходи : " +
                $"{analyticsService.GetTotalIncome(today, tomorrow):N2}");

            Console.WriteLine(
                $"Витрати : " +
                $"{analyticsService.GetTotalExpenses(today, tomorrow):N2}");

            Console.WriteLine("Витрати за категоріями:");

            var expenses = analyticsService.GetExpensesByCategory(
                today, tomorrow);

            if (expenses.Count == 0)
            {
                Console.WriteLine("Витрат за сьогодні немає.");
            }
            else
            {
                foreach (var item in expenses)
                {
                    Console.WriteLine(
                        $"{item.Key}: {item.Value:N2}");
                }
            }

            break;
        }

        case "5":
        {
            Console.Write("Категорія: ");
            string category = Console.ReadLine() ?? string.Empty;

            Console.Write("Максимальний бюджет на місяць: ");

            if (!decimal.TryParse(
                Console.ReadLine(), out decimal maxAmount) ||
                maxAmount <= 0)
            {
                Console.WriteLine("Введіть правильну суму.");
                break;
            }

            try
            {
                var limit = budgetService.SetLimit(
                    category, maxAmount);

                Console.WriteLine(
                    $"Ліміт для \"{limit.CategoryId}\" установлено: " +
                    $"{limit.MaxAmount:N2}");

                Console.WriteLine(
                    $"Вже витрачено: {limit.CurrentAmount:N2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            break;
        }

        case "6":
        {
            var limits = budgetService.GetAllLimits().ToList();

            Console.WriteLine("=== Ліміти бюджета ===");

            if (limits.Count == 0)
            {
                Console.WriteLine("Ліміти ще не встановлено.");
                break;
            }

            foreach (var limit in limits)
            {
                decimal remaining = limit.MaxAmount - limit.CurrentAmount;

                Console.WriteLine(
                    $"{limit.CategoryId}: ліміт {limit.MaxAmount:N2}, " +
                    $"витрачено {limit.CurrentAmount:N2}, " +
                    $"залишок {remaining:N2}");
            }

            break;
        }

        case "7":
        {
            Console.Write("Назва фінансової цілі: ");
            string title = Console.ReadLine() ?? string.Empty;

            Console.Write("Необхідна сума: ");

            if (!decimal.TryParse(
                Console.ReadLine(), out decimal targetAmount) ||
                targetAmount <= 0)
            {
                Console.WriteLine("Введіть правильну суму.");
                break;
            }

            Console.Write("Дата досягнення мети (дд.мм.рррр): ");
            string dateInput = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!DateTime.TryParseExact(
                dateInput,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime targetDate))
            {
                Console.WriteLine(
                    "Невірний формат дати. Приклад: 01.02.2027");
                break;
            }

            try
            {
                var goal = goalService.CreateGoal(
                    title, targetAmount, targetDate);

                Console.WriteLine(
                    $"Ціль \"{goal.Title}\" створена. ID: {goal.Id}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            break;
        }

        case "8":
        {
            var goals = goalService.GetAllGoals().ToList();

            if (goals.Count == 0)
            {
                Console.WriteLine("Спочатку створіть фінансову мету.");
                break;
            }

            foreach (var goal in goals)
            {
                Console.WriteLine(
                    $"{goal.Id} — {goal.Title}: " +
                    $"{goal.CurrentAmount:N2} / {goal.TargetAmount:N2}");
            }

            Console.Write("Введіть ID цілі: ");
            string goalId = Console.ReadLine() ?? string.Empty;

            Console.Write("Сума поповнення: ");

            if (!decimal.TryParse(
                Console.ReadLine(), out decimal savings) ||
                savings <= 0)
            {
                Console.WriteLine("Введіть правильну суму.");
                break;
            }

            try
            {
                goalService.AddSavings(goalId, savings);

                Console.WriteLine(
                    $"Поповнення виконане. Прогрес: " +
                    $"{goalService.GetProgressPercent(goalId):N1}%");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            break;
        }

        case "9":
        {
            var goals = goalService.GetAllGoals().ToList();

            Console.WriteLine("=== Фінансові цілі ===");

            if (goals.Count == 0)
            {
                Console.WriteLine("Фінансові цілі не створені.");
                break;
            }

            foreach (var goal in goals)
            {
                Console.WriteLine(
                    $"ID: {goal.Id} | {goal.Title} | " +
                    $"{goal.CurrentAmount:N2} / {goal.TargetAmount:N2} | " +
                    $"Строк: {goal.TargetDate:yyyy-MM-dd} | " +
                    $"Прогрес: " +
                    $"{goalService.GetProgressPercent(goal.Id):N1}%");
            }

            break;
        }

        case "10":
        {
            Console.Write("Назва сімейної групи: ");
            string familyName = Console.ReadLine() ?? string.Empty;

            try
            {
                familyService.CreateFamilyGroup(familyName);

                var family = data.FamilyGroups.LastOrDefault(
                    f => f.Name == familyName.Trim());

                if (family != null)
                {
                    currentFamilyId = family.Id;

                    if (!string.IsNullOrEmpty(currentUserId))
                    {
                        familyService.AddMember(
                            currentFamilyId, currentUserId);
                    }

                    Console.WriteLine(
                        $"Група \"{family.Name}\" створена. ID: {family.Id}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            break;
        }

        case "11":
        {
            if (string.IsNullOrEmpty(currentFamilyId))
            {
                Console.WriteLine(
                    "Спершу створіть сімейну групу.");
                break;
            }

            Console.WriteLine("Зареєстровані користувачі:");

            foreach (var registeredUser in data.Users)
            {
                Console.WriteLine(
                    $"{registeredUser.Id} — {registeredUser.Username} " +
                    $"({registeredUser.Email})");
            }

            Console.Write("Введіть ID користувача: ");
            string memberId = Console.ReadLine() ?? string.Empty;

            try
            {
                familyService.AddMember(currentFamilyId, memberId);
                Console.WriteLine("Учасник доданий до групи.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            break;
        }

        case "0":
            running = false;
            Console.WriteLine("Дякуємо за використання FamilyWallet!");
            break;

        default:
            Console.WriteLine("Невідома команда. Виберіть пункт меню.");
            break;
    }
}