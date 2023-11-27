## Simple Product Inventory API

This comprehensive product inventory API provides not only basic CRUD operations (Create, Read, Update, Delete) for managing product data but also incorporates several advanced features to enhance its functionality and usability.

### Key Features

#### API Versioning

- **Header-based API Versioning:** Specify the desired API version in the request header using the `X-API-Version` header.

- **URL-based API Versioning:** Include the API version segment in the URL path.

- **Query String-based API Versioning:** Append the API version parameter (`api-version`) to the query string.

#### Global Searching

- Perform full-text searches across product names, descriptions, and other relevant fields.

#### Pagination

- Retrieve paginated results sets to handle large product inventories efficiently.

- Control the number of items per page using the `size` and `page` parameters.

#### HTTPS Communication

- Enforce secure HTTPS connections for all API requests to safeguard sensitive data.

#### Swagger Configuration for API Versioning

- Leverage Swagger documentation to generate API documentation for different API versions.

#### Sorting Items

- Sort product listings by various criteria, such as name, price, or creation date using the `sortBy` and `sortOrder`.

#### Filtering Items

- Filter product listings based on specific criteria:

    - **Name:** Filter products based on their name or its partial match.

    - **SKU:** Filter products based on their unique SKU identifier.

    - **Price Range:** Filter products based on a price range, specifying both minimum and maximum prices.
