function solve(firstNameInput, lastNameInput, ageInput) { 

    let person = {
        firstName: firstNameInput,
        lastName: lastNameInput,
        age: ageInput,
    }
    return person;
}
console.log(solve("Peter", "Pan", "20"));