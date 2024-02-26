function solve(inputNum) {
    "use strict";

    let oddSum = 0;
    let evenSum = 0;

    while (inputNum > 0) {
        const currDigit = inputNum % 10

        if (currDigit % 2 === 0) {
            evenSum += currDigit
        } else {
            oddSum += currDigit
        }

        inputNum = Math.floor(inputNum / 10)
    }
    console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`)
}
solve(1000435);