# SpecificationExpression 🧩

<p align="center">
  <img src="https://img.shields.io/badge/Unity-2020.3+-000000.svg?style=flat-square&logo=unity" alt="Unity" />
  <img src="https://img.shields.io/badge/Language-C%23_8.0-239120.svg?style=flat-square&logo=c-sharp" alt="C#" />
  <img src="https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square" alt="License" />
</p>

<p align="center">
  SpecificationExpression is a clean-architecture framework for Unity that decouples business logic from LINQ queries. It provides composable Specifications, Expression Tree builders, and ScriptableObject wrappers to create dynamic, serializable, and designer-friendly game logic.
</p>

---

**Turn your Game Logic into Data.**

A lightweight, type-safe implementation of the **Specification Pattern** for Unity, powered by C# **Expression Trees**.

**SpecificationExpression** solves the problem of hardcoded `Where(x => ...)` LINQ queries. It allows you to build complex, dynamic filters (Inventory, Quests, Achievements) that are reusable, testable, and configurable directly in the Unity Inspector.

## 🚀 Key Features

* **Composable Logic:** Chain business rules dynamically (`.And()`, `.Or()`, `.Not()`) without writing spaghetti `if/else` blocks.
* **Unity Inspector Integration:** Wrap Specifications in **ScriptableObjects** so Game Designers can tweak logic without touching code.
* **Dynamic Query Builders:** Create complex runtime filters (e.g., E-commerce style sorting for Inventories) easily.
* **High Performance:** Uses compiled Expression Trees to avoid the overhead of standard Reflection when doing property lookups.

## 📂 Structure

```text
Assets/SpecificationExpression/
├── Core/                  # Base Specifications, Interfaces & Expression Visitors
├── Modules/
│   ├── DynamicSearch/     # Runtime Query Builder (for UI Filters)
│   ├── DesignerConfig/    # ScriptableObject Wrappers (for Inspector)
│   └── GenericAttribute/  # String-based Property Specification (for JSON/Config)
└── Demo/                  # Usage Examples
```
## 📦 Installation
Simply clone this repository and copy the SpecificationExpression folder into your Unity project's Assets directory.

## 📖 Usage Examples

### 1. The Code Way (Clean & Reusable)
Define rules once, use them everywhere (LINQ, Unit Tests, Single Checks).
```csharp
// Define simple specs
var rareSpec = new RaritySpec(4);
var weaponSpec = new ItemTypeSpec(ItemType.Weapon);

// Combine them cleanly
var legendaryWeaponSpec = rareSpec.And(weaponSpec);

// Use with IQueryable/IEnumerable
var results = inventory.AsQueryable()
                       .Where(legendaryWeaponSpec.ToExpression())
                       .ToList();
```

### 2. The Unity Way (Designer Friendly)

Expose logic to the Inspector using ScriptableObjects.

1. Create a FilterAsset (inherits from SpecAsset).
2. Create a composite asset (e.g., "Legendary Swords") via the Create Menu.
3. Drag and drop it into your GameObjects.

```csharp
public class MagicChest : MonoBehaviour
{
    [SerializeField] private ItemFilterAsset _lootLogic; // Set by Designer

    public void GenerateLoot()
    {
        // Convert Asset to Expression and run
        var spec = _lootLogic.GetSpec();
        var loot = allItems.Where(spec.ToExpression().Compile()).ToList();
    }
}
```

### 3. The Dynamic Way (UI Filters)
Build queries at runtime based on user input.

```csharp
var builder = new SearchQueryBuilder<Card>();

if (ui.FireToggle.isOn) builder.AddFilter(new ElementSpec("Fire"));
if (ui.CostSlider.value < 5) builder.AddFilter(new MaxCostSpec(5));

// Generates: x => x.Element == "Fire" && x.Cost < 5
var finalFunc = builder.Build();
```

## ⚠️ Performance Note
This framework leverages Expression.Compile(), which has a small CPU cost.

- Recommended: Compile specifications during initialization or upon user interaction (e.g., applying a filter).
- Avoid: Compiling expressions every frame inside Update().

## 📄 License
This project is free to use. No license is required. Author: thanhthai18
