# Code style
Этот документ описывает стиль кода, применяемый при разработке.

```csharp
// Using:
// - Находятся в самом начале файла, вне объявления пространства имен (namespace).
// - Поддерживайте их в порядке, удаляйте неиспользуемые директивы.
// - Не забывать оборачивать препроцессор-зависимые объявления в препропроцессорную директиву
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Namespaces:
// - Для объявления простанства имен используете PascalCase, без спец-символов, нижних подчеркиваний (_) и дефисов (-).
// - Для объявления под-пространств используйте точку (.), например MyApplication.GameFlow, MyApplication.AI, и так далее.
namespace Namespace.Example
{

    // Enum:
    // - PascalCase.
    // - Без префиксов и суффиксов.
    // - Существительное в единственном числе.
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    // Flags enum:
    // - PascalCase.
    // - Без префиксов и суффиксов.
    // - Существительное во множественном числе.
    // - Двоичная репрезентация для примера, писать ее не обязательно.
    [Flags]
    public enum AttackModes
    {
        None = 0,                          // 000000
        Melee = 1,                         // 000001
        Ranged = 2,                        // 000010
        Special = 4,                       // 000100

        MeleeAndSpecial = Melee | Special  // 000101
    }

    // Interfaces:
    // - Именуйте интерфейсы фразами с прилагательными.
    // - Используйте 'I' префикс.
    public interface IDamageable
    {
        // Properties:
        // - PascalCase, без специальных символов.
        float DamageValue { get; }

        // Methods:
        // - Начинайте имя метода с глаголов или фраз с глаголами, чтобы показать действие.
        // - Используйте camelCase для имен параметров.
        bool ApplyDamage(string description, float damage, int numberOfHits);
    }

    // Generic Interfaces:
    // - Используйте 'T' префикс для обобщенного типа.
    // - Указывайте ковариантность/контравариантность, где применимо.
    public interface IDamageable<in TDamage, out TResult>
    {
        TResult Damage(TDamage damage);
    }

    // Classes or structs:
    // - Именуйте их существительными или существительными фразами.
    // - Избегайте префиксов.
    // - Один MonoBehaviour на файл. Если у вас есть MonoBehaviour в файле, имя исходного файла должно соответствовать.
    public class StyleExample : MonoBehaviour
    {
        // Общее:
        // - Избегайте специальных символов (обратные слеши, символы, Unicode-символы).
        // - Используйте осмысленные имена. Не сокращайте (если это не математика).
        // - Приставляйте к булевым значениям глагол, например глагол to be (is/was, are/were) или can.
        //   Булевы значения задают вопрос, который можно ответить true или false.
        // - Всегда указывайте модификатор доступа к полю.
        // - Используйте существительные для имен полей.
        // - Используйте PascalCase для public и protected полей.
        // - Используйте _camelCase для приватных (private) переменных с нижнем подчеркиванием (_) в качестве префикса.
        // - Общий порядок объявлений полей/свойств/методов в зависимости от модификаторов доступа:
        //   public -> protected -> private, если не указано иначе.
        // - Члены класса не должны иметь назваение класса в своем имени.


        // Inner classes/structs
        // - Вложенные структуры и классы объявляются в самом начале
        // - В остальном следуют правилам данному code style guide
        [Serializable]
        public class InnerClassExample
        {
            private readonly string _someName;

            public InnerClassExample(string someName)
            {
                _someName = someName;
            }
        }

        // Constants:
        // - Используйте PascalCase для объявления костант.
        public const string DamageType = "SomeType";
        protected const float HealthOverTime = 5.5f;
        private const int HardcodedNumber = 5;

        // Static Fields:
        // - Предпочитайте expression-bodied синтаксис, где применимо.
        public static PlayerStats DefaultStats => new PlayerStats();
        protected static float DamageTaken;
        private static bool _isAlive;

        // Serialized fields:
        // - Сериализованные поля объявляются раньше не сериализованных, но следуют тому же порядку.
        // - Аттрибуты объявляются на одной строке, раздельно от объявления члена класса.
        // - Если аттрибутов несколько или они требуют много аргуметов - можно объявить их в несколько строк.
        [SerializeField]
        protected PlayerStats Stats;
        [Tooltip("Is current player dead.")]
        [SerializeField]
        private bool _isPlayerDead = false;

        [Range(0f, 1f)]
        public float RangedStat;    // Избегайте публичных полей в пользу свойств
        protected bool CanShoot = true;
        private int _elapsedTimeInDays;
        private int _health;
        private List<int> _shopIds;


        // Properties:
        // - Предпочтительнее публичного поля.
        // - PascalCase, без специальных символов.
        // - Используйте expression-bodied синтаксис, где применимо.
        // - В остальном следует стилю объявления полей.

        // Только для чтения (read-only), возвращает значение приватного поля.
        // Expression-bodied синтаксис.
        public bool IsPlayerDead => _isPlayerDead;

        // Только для чтения, эквивалентно предыдущему в случае, когда нет необходимости в сериализации/отображении в инспекторе:
        public bool IsPlayerDead { get; private set; }

        // Явная релизация геттера и сеттера
        public bool IsPlayerDead
        {
            get => _isPlayerDead;
            set => _isPlayerDead = value;
        }
        
        // Используйте встроенные абстракции коллекций при доступе к ним извне
        // Например, IReadOnlyList/IList, IReadOnlyCollection/ICollection, IEnumerable
        public IList<int> ShopIds => _shopIds;

        // Только для записи (write-only) без backing field
        protected int Health { private get; set; }

        // Авто-свойство с инициализацией значением по умолчанию
        private string DescriptionName { get; set; } = "Fireball";

        // Events:
        // - PascalCase
        // - Именуйте фразой с глаголом.
        // - Герундий для действий "до", прошедшее время для действий "после".
        // - Используйте делегат System.Action для большинства событий (может принимать от 0 до 16 параметров).
        // - Методы вызова событий формируются как префикс "On" + название события.

        // Событие "до" (present participle, -ing)
        public event Action OpeningDoor;

        // Событие "после" (past simple, -ed + исключения)
        public event Action<int> DoorOpened;

        // Constructor
        // - И аналогичные констуктуру методы первичной инициализации.
        // - Расположены самыми первыми среди других методов.
        public StyleExample(int currentHp)
        {
            _health = currentHp;
        }

        // Метод инициализации MonoBehaviour в виду отсутствия конструктора.
        public void Construct(int currentHp)
        {
            _health = currentHp;
        }

        // Unity callbacks
        // - Сначала идут Unity события жизненного цикла в порядке их вызова
        // - Далее Update события

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void OnDestroy()
        {
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {
        }

        // Methods:
        // - Начинайте имя метода с глаголов или фраз с глаголами, чтобы показать действие.
        // - Имена параметров camelCase.

        // Методы начинаются с глагола.
        public void SetInitialPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Методы задают вопрос, когда они возвращают bool.
        // Такие методы должны быть без побочных-эффектов, 
        // то есть не должны изменять состояние объекта.
        public bool IsNewPosition(Vector3 newPosition)
        {
            return (transform.position == newPosition);
        }

        // Метод вызова события.
        protected void OnDoorOpened()
        {
            DoorOpened?.Invoke(_health);
        }

        private void FormatExamples(int someExpression)
        {
            // Используем guard-clause для избежания вложенности
            if(someExpression < 0)
            {
                return;   
            }
            
            // Var:
            // - Используйте var, если это способствует читаемости, особенно с длинными именами типов.
            var powerUps = new List<PlayerStats>();
            var dict = new Dictionary<string, List<GameObject>>();


            // Switch:
            switch (someExpression)
            {
                case 0:
                    // ..
                    break;
                case 1:
                    // ..
                    break;
                case 2:
                    // ..
                    break;
            }

            // Braces:
            // - Всегда сохраняйте фигурные скобки для ясности.
            // - Открывающая и закрывающая скобки на отдельных строках (Allman-стиль)
            for (int i = 0; i < 100; i++)
            {
                DoSomething(i);
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DoSomething(j);
                }
            }

            if (_health > 0)
            {
                _isPlayerDead = false;
            }
            else
            {
                _isAlive = false;
            }

            // Local methods:
            // - Используйте локальные методы для упрощения чтения метода
            // - Используйте их в том случае, если данный метод больше нигде не используется
            // - В противном случае его лучше сделать обычноым методом
            void DoSomething(int x)
            {
                // ..
            }
        }
    }

    // Other classes/structs:
    // - Вспомогательные классы/структуры/enum
    // - Следуют стилю, каждый в отдельном файле
    [Serializable]
    public struct PlayerStats
    {
        public int MovementSpeed;
        public int HitPoints;
        public bool HasHealthPotion;
    }

}
```

## Коментарии
Для комментариев, поясняющих поведение сущности, используем обычный комментарий.
Если требуется внимание (доработка) к определенному участку кода, то используем `//TODO` комментарий.


## Rider
### Настройка папок в качестве namespace provider
В Rider можно указать источники пространства имен при автоматическом рефакторинге. По умолчанию структура папок в проекте является источником пространства имен, однако такой способ порой создает слишком длинные namespaces.
Можно исключить папку из генерации пространства имен следующим образом:
- ПКМ по папке
- Properties
- Убрать галочку с Namespace Provider
