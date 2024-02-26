function solve(speed, area) {
    "use strict";

    let zone = 0;

    if (area === 'motorway') {
        zone = 130;
    } else if (area === 'interstate') {
        zone = 90;
    } else if (area === 'city') {
        zone = 50;
    } else if (area === 'resedential') {
        zone = 20;
    }

    const speedLimitDiff = speed - zone;

    if (speedLimitDiff <= 0) {
        console.log(`Driving ${speed} km/h in a ${zone} zone`)
    } else {
        let speedingStatus = ''; 

        if (speedLimitDiff > 0 && speedLimitDiff <= 20) {
            speedingStatus = 'speeding';
        } else if (speedLimitDiff > 20 && speedLimitDiff <= 40) {
            speedingStatus = 'excessive speeding';
        } else {
            speedingStatus = 'reckless driving';
        }
        console.log(`The speed is ${speedLimitDiff} km/h faster than the allowed speed of ${zone} - ${speedingStatus}`);
    }
}
solve(21, 'residential'); 