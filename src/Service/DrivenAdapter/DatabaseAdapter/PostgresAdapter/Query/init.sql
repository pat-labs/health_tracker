-- Create the food_item table (if it doesn't exist)
CREATE TABLE IF NOT EXISTS food_item (
    food_item_id VARCHAR(19) PRIMARY KEY, -- Use UUID for ID
    name VARCHAR(255) NOT NULL,
    calories_per_100g DECIMAL(10, 2), -- Use DECIMAL for precise decimal values
    protein_per_100g DECIMAL(10, 2),
    carbs_per_100g DECIMAL(10, 2),
    fat_per_100g DECIMAL(10, 2)
);

-- Example insert (optional)
INSERT INTO food_item (food_item_id, name, calories_per_100g, protein_per_100g, carbs_per_100g, fat_per_100g)
VALUES ('1873071922225348608', 'Example Food', 200.50, 10.25, 25.75, 5.00);

SELECT f.food_item_id, f.calories_per_100g, f.carbs_per_100g, f.fat_per_100g, f.name, f.protein_per_100g
FROM food_item AS f