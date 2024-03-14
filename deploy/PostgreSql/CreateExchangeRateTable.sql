CREATE TABLE IF NOT EXISTS exchangerates (
    id UUID PRIMARY KEY,
    from_currency_name VARCHAR(255),
    from_currency_code VARCHAR(10),
    to_currency_name VARCHAR(255),
    to_currency_code VARCHAR(10),
    rate DOUBLE PRECISION,
    bid_price DOUBLE PRECISION,
    ask_price DOUBLE PRECISION
);