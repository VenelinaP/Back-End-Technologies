function solve(age) {
    "use static"

    let mesage = "";

    if (age >= 0 && age <=2) {
        mesage = "baby";
    } else if (age >= 3 && age <= 13) {
        mesage = "child";
    } else if (age >=14 && age <= 19) {
        mesage = "teenager";
    } else if (age >= 20 && age <= 65) {
        mesage = "adult";
    } else if (age >= 66) {
        mesage = "elder"
    } else {
        mesage = "out of bounds";
    }
    console.log(mesage)
}
solve(1);