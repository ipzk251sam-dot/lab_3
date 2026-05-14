using System;

public abstract class Hero
{
    public string Name { get; protected set; }
    public abstract void ShowInventory();
}

public class Warrior : Hero { public Warrior() { Name = "Warrior"; } public override void ShowInventory() => Console.WriteLine(Name); }
public class Mage : Hero { public Mage() { Name = "Mage"; } public override void ShowInventory() => Console.WriteLine(Name); }
public class Palladin : Hero { public Palladin() { Name = "Palladin"; } public override void ShowInventory() => Console.WriteLine(Name); }

public abstract class InventoryDecorator : Hero
{
    protected Hero _hero;
    public InventoryDecorator(Hero hero) { _hero = hero; }
}

public class WeaponDecorator : InventoryDecorator
{
    private string _weapon;
    public WeaponDecorator(Hero hero, string weapon) : base(hero) { _weapon = weapon; }
    public override void ShowInventory()
    {
        _hero.ShowInventory();
        Console.WriteLine($" + Зброя: {_weapon}");
    }
}

public class ClothingDecorator : InventoryDecorator
{
    private string _clothing;
    public ClothingDecorator(Hero hero, string clothing) : base(hero) { _clothing = clothing; }
    public override void ShowInventory()
    {
        _hero.ShowInventory();
        Console.WriteLine($" + Одяг: {_clothing}");
    }
}

public class ArtifactDecorator : InventoryDecorator
{
    private string _artifact;
    public ArtifactDecorator(Hero hero, string artifact) : base(hero) { _artifact = artifact; }
    public override void ShowInventory()
    {
        _hero.ShowInventory();
        Console.WriteLine($" + Артефакт: {_artifact}");
    }
}