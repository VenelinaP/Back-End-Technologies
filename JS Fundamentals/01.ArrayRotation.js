function solve(inputArr, numOfRotation) {
    "use strict";

  for (let index = 0; index < numOfRotation; index++) {

    const firstEl = inputArr.shift()
    inputArr.push(firstEl) //така взима 1 елемент и го пусхва накрая
  }
  console.log(inputArr.join(" "));
}

solve([51, 47, 32, 61, 21], 2);
