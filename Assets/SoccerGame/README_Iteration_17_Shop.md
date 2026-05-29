# Iteration 17 — Валюта + магазин скинов

## Что изменилось
- Добавлен `CoinManager.cs` — DontDestroyOnLoad, 1 гол = 1 монета, во всех режимах
- Добавлен `CoinUI.cs` — отображение монет в игре
- Добавлен `SkinManager.cs` — DontDestroyOnLoad, управление скинами (покупка, экипировка, PlayerPrefs)
- Добавлен `SkinShopUI.cs` — панель магазина в меню
- Добавлен `SkinShopItem.cs` — элемент магазина (превью, цена, BUY/EQUIP/EQUIPPED)
- Добавлен `BallSkinApplier.cs` — применяет активный скин на мяч при старте
- Обновлён `MainMenuUI.cs` — кнопка SHOP
- Создаётся префаб `Assets/SoccerGame/Prefabs/SkinItem.prefab`

## Как настроить

### Шаг 1 — Game сцена
1. Открой Game сцену
2. Меню **SoccerGame → Setup Coins + Skins - Game (Iteration 17)**
3. Сохрани сцену

### Шаг 2 — Menu сцена
1. Меню **SoccerGame → Setup Shop - Menu (Iteration 17)**
2. Сохранится автоматически

## 6 скинов
| ID | Название | Цена | Цвет |
|---|---|---|---|
| default | Classic | 0 (бесплатный) | Белый |
| fire | Fire | 10 | Оранжевый |
| ice | Ice | 15 | Голубой |
| toxic | Toxic | 20 | Зелёный |
| gold | Gold | 30 | Золотой |
| shadow | Shadow | 50 | Фиолетовый |

## Как тестировать
1. Запусти с MainMenu
2. 4 кнопки: MATCH, ENDLESS, LEVELS, SHOP
3. SHOP → список скинов, Classic уже куплен и экипирован
4. Играй в любой режим — за каждый забитый гол +1 монета
5. Монеты отображаются справа вверху в игре и в магазине
6. В магазине: BUY → покупает, EQUIP → экипирует, EQUIPPED → серая
7. Скин применяется на мяч (цвет + свечение)

## Как поменять арт скинов
- Префаб: `Assets/SoccerGame/Prefabs/SkinItem.prefab`
- Скины определены в `SkinManager.cs` → метод `CreateDefaultSkins()`
- Для спрайтов вместо цветов — добавь поле `Sprite` в `SkinData` и обнови `BallSkinApplier`

## Ожидаемый результат
Валюта за голы, магазин скинов в меню, скины применяются на мяч визуально.
