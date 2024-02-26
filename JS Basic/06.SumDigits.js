function solve(num) {
    "use strict";

    const numAsString = num.toString();
    let totalSum = 0;

    for (const char of numAsString) {
        const charAsNumber = parseInt(char, 10)
        totalSum += charAsNumber;
        
    }
    console.log(totalSum);
}
solve(245678)