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