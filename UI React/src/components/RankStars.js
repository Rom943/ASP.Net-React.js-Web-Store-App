import { AiFillStar, AiOutlineStar } from 'react-icons/ai';

// Function to render star icons based on the given rank
function RankStars(rank) {
    // Round the rank to the nearest integer
    const roundedRank = Math.round(rank.rank);

    // Styles for the stars container
    const starsContainer = {
        "display": "flex"
    };

    // Render the star icons
    return (
        <>
            {/* Check if rank is provided */}
            {rank ? (
                <div style={starsContainer}>
                    {/* Render filled stars based on the rounded rank */}
                    {Array.from({ length: roundedRank }).map((_, idx) => (
                        <AiFillStar key={idx} />
                    ))}
                    {/* Render outline stars for the remaining empty stars */}
                    {Array.from({ length: 5 - roundedRank }).map((_, idx) => (
                        <AiOutlineStar key={idx} />
                    ))}
                </div>
            ) : (
                <div></div>
            )}
        </>
    );
}

export default RankStars;
