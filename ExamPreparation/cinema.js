function cinema(input) {
    const moviesCount = parseInt(input[0]);
    const allMovies = input.slice(1, moviesCount + 1);
    const allCommands = input.slice(moviesCount + 1);

    for (let index = 0; index < allCommands.length; index += 1) {
        const rawParams = allCommands[index].split(' ');
        const commandName = rawParams[0];

        if (commandName === 'Sell') {
            const soldMovie = allMovies.shift();
            console.log(`${soldMovie} ticket sold!`);

        } else if (commandName === 'Add') {
            const movieTitleToAdd = rawParams.slice(1).join(' ');
            allMovies.push(movieTitleToAdd);

        } else if (commandName === 'Swap') {
            const firstIndex = parseInt(rawParams[1], 10);
            const secondIndex = parseInt(rawParams[2], 10);

            if (firstIndex < 0 || firstIndex >= allMovies.length) {
                continue;
            }
            if (secondIndex < 0 || secondIndex >= allMovies.length) {
                continue;
            }

            const movieOnFirstIndex = allMovies[firstIndex];
            allMovies[firstIndex] = allMovies[secondIndex];
            allMovies[secondIndex] = movieOnFirstIndex;

            console.log('Swapped!');

        } else if (commandName === 'End') {
            break;
        }
    }
    if (allMovies.length) {
        console.log(`Tickets left: ${allMovies.join(', ')}`);

    } else {
        console.log('The box office is empty');
    }
}
cinema(['3','Avatar', 'Titanic', 'Joker', 'Sell', 'End', 'Swap 0 1']);
