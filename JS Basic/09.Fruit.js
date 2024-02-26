function solve(fruitType, weight, pricePerKilogram) {
    "use static";

    const pricePerGram = pricePerKilogram / 1000;
    const totalPrice = weight * pricePerGram;
    const weightInKilos = weight / 1000;

    console.log(`I need $${totalPrice.toFixed(2)} to buy ${weightInKilos.toFixed(2)} kilograms ${fruitType}.`)
}
solve('orange', 2500, 1.80);