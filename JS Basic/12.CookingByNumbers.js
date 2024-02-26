function solve(rawNum, operation1, operation2, operation3, operation4, operation5) {
    "use strict";
    
    let number = parseInt(rawNum, 10);
    
    if (operation1 === 'chop') {
        number = number / 2;
    } else if (operation1 === 'dice') {
        number = Math.sqrt(number);
    } else if (operation1 === 'spice') {
        number += 1;
    } else if (operation1 === 'bake') {
        number = number * 3;
    } else if (operation1 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number)
    if (operation2 === 'chop') {
        number = number / 2;
    } else if (operation2 === 'dice') {
        number = Math.sqrt(number);
    } else if (operation2 === 'spice') {
        number += 1;
    } else if (operation2 === 'bake') {
        number = number * 3;
    } else if (operation2 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number)
    if (operation3 === 'chop') {
        number = number / 2;
    } else if (operation3 === 'dice') {
        number = Math.sqrt(number);
    } else if (operation3 === 'spice') {
        number += 1;
    } else if (operation3 === 'bake') {
        number = number * 3;
    } else if (operation3 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number)
    if (operation4 === 'chop') {
        number = number / 2;
    } else if (operation4 === 'dice') {
        number = Math.sqrt(number);
    } else if (operation4 === 'spice') {
        number += 1;
    } else if (operation4 === 'bake') {
        number = number * 3;
    } else if (operation4 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number)
    if (operation5 === 'chop') {
        number = number / 2;
    } else if (operation5 === 'dice') {
        number = Math.sqrt(number);
    } else if (operation5 === 'spice') {
        number += 1;
    } else if (operation5 === 'bake') {
        number = number * 3;
    } else if (operation5 === 'fillet') {
        number = number * 0.8;
    }
    console.log(number)
}
solve('32', 'chop', 'chop', 'chop', 'chop', 'chop');
//solve('9', 'dice', 'spice', 'chop', 'bake', 'fillet');