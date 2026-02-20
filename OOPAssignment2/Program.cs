#region p1q1
//a
//public fields allow external modifications & no proper validation in withdraw method (to check balance availability)

//b
//make fields private & provide setters and getters instead
//validate whether balance is sufficient before allowing withdrawal

//c
//because public fields allow uncontrolled modifications which can lead to invalid state of object
#endregion

#region p1q2
//field member is variable that holds data
//property is member that provides access to private field to read or write or even validate
//yes, a property can contain logic
//public int salary { get { return initialSalary + bonus; } }
#endregion

#region p1q3
//a)Indexer, allows indexed access to object like an array - obj[i] 
//b) throws indexOutOfRangeException 
//to make safe:
//public string this[int index]
//{
//    get
//    {
//        if (index < 0 || index >= names.Length)
//            throw new ArgumentOutOfRangeException(nameof(index));
//        return names[index];
//    }
//    set
//    {
//        if (index < 0 || index >= names.Length)
//            throw new ArgumentOutOfRangeException(nameof(index));
//        names[index] = value;
//    }
//}
//c)yes, indexers can be overloaded with different parameter types and counts
//for example: public int this[string name] &  public string this[int index]
//indexer allows access using name & other using index
#endregion