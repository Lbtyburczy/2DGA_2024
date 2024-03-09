/// <summary>
/// Is an interface that can be put on any object that should be able to take damage from a source
/// 
/// An interface in Unity is a type of script that defines functionality for it's implementers.
/// Essentially, it’s a list of functions that will be required by any class that implements the interface.
/// When a class implements an interface, it must include all of these functions, publicly, using the same method names, parameters and return types as written in the interface script.
/// </summary>

public interface IDamageable
{
   void TakeDamage(int damage);
}
