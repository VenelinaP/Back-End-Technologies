function solve(input) {
   "use strict";

    const typeParam = typeof input
     if (typeParam === "number") {
        const area = input ** 2 * Math.PI;
        console.log(area.toFixed(2));
     } else {
        console.log(`We can not calculate the circle area, because we received a ${typeParam}.`)
     }
}
solve(5)
//solve("name")