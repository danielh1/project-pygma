namespace Pygma.UatTests.Asserters
{
    public static class ObjectExtensions
    {
        // public static void DeepAssertValueTypes(this object obj, object another)
        // {
        //     if (ReferenceEquals(obj, another))
        //     {
        //         Assert.True(true);
        //         return;
        //     }
        //
        //     if ((obj == null) || (another == null))
        //     {
        //         throw new Xunit.Sdk.XunitException();
        //     }
        //
        //     //Compare two object's class, return false if they are difference
        //     if (obj.GetType() != another.GetType())
        //     {
        //         throw new Xunit.Sdk.XunitException();
        //     }
        //     
        //     //Get all properties of obj
        //     //And compare each other
        //     foreach (var property in obj.GetType().GetProperties())
        //     {
        //         var objValue = property.GetValue(obj);
        //         var anotherValue = property.GetValue(another);
        //
        //         if (objValue is null)
        //         {
        //             continue;
        //         }
        //
        //         var t = objValue.GetType();
        //
        //         if(t.IsValueType)
        //         {
        //             objValue.Should().Be(anotherValue);
        //         }
        //     }
        //
        //     Assert.True(true);
        // }
    }
}