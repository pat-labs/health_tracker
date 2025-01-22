-- Create the database (if it doesn't exist)
CREATE DATABASE IF NOT EXISTS health_tracker;

-- Connect to the database
\c health_tracker;

-- Create the food_item table (if it doesn't exist)
CREATE TABLE IF NOT EXISTS food_item (
    food_item_id UUID PRIMARY KEY DEFAULT gen_random_uuid(), -- Use UUID for ID
    name VARCHAR(255) NOT NULL,
    calories_per_100g DECIMAL(10, 2), -- Use DECIMAL for precise decimal values
    protein_per_100g DECIMAL(10, 2),
    carbs_per_100g DECIMAL(10, 2),
    fat_per_100g DECIMAL(10, 2)
);

-- Example insert (optional)
INSERT INTO food_item (name, calories_per_100g, protein_per_100g, carbs_per_100g, fat_per_100g)
VALUES ('Example Food', 200.50, 10.25, 25.75, 5.00);