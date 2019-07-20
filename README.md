# NASA Mars Rover API Integration

An API that integrates with the NASA API to pull images taken by a rover on Mars and
save them to a default or specified location.

## Requirements

- .NET 4.7.2
- RestSharp (free on nuget)
- Newtonsoft.Json (free on nuget)
- Swashbuckle (free on nuget)

## Swagger documentation

Swagger test harness -> https://localhost:{port}/swagger

Swagger document -> https://localhost:{port}/swagger/docs/v1

```json
{
  "swagger": "2.0",
  "info": {
    "version": "v1",
    "title": "NASA.API"
  },
  "host": "localhost:44378",
  "schemes": ["https"],
  "paths": {
    "/nasa/images": {
      "get": {
        "tags": ["Nasa"],
        "summary": "Get images from the NASA API. Values passed through the\r\nimageRequest will be populated into a call to the NASA API.",
        "operationId": "Nasa_GetImagesByDate",
        "consumes": [],
        "produces": [
          "application/json",
          "text/json",
          "application/xml",
          "text/xml"
        ],
        "parameters": [
          {
            "name": "imageRequest.apiKey",
            "in": "query",
            "description": "The key from NASA granting API access. \r\nCan pass 'DEMO_KEY' for testing purpose, but this is subject to a lower rate limit.",
            "required": true,
            "type": "string"
          },
          {
            "name": "imageRequest.rover",
            "in": "query",
            "description": "The rover that's taking the images.",
            "required": true,
            "type": "string"
          },
          {
            "name": "imageRequest.imageDate",
            "in": "query",
            "description": "The date of the images. Searches the earth_date field\r\nfrom NASA.",
            "required": true,
            "type": "string",
            "format": "date-time"
          },
          {
            "name": "imageRequest.savePath",
            "in": "query",
            "description": "An optional path where to save the images.\r\nIf this field is empty, the files are saved to c:/NASA/Images/{date}",
            "required": false,
            "type": "string"
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "schema": {
              "type": "object"
            }
          }
        }
      }
    }
  },
  "definitions": {
    "NasaPhotoRequest": {
      "required": ["apiKey", "rover", "imageDate"],
      "type": "object",
      "properties": {
        "apiKey": {
          "description": "The key from NASA granting API access. \r\nCan pass 'DEMO_KEY' for testing purpose, but this is subject to a lower rate limit.",
          "type": "string"
        },
        "rover": {
          "description": "The rover that's taking the images.",
          "type": "string"
        },
        "imageDate": {
          "format": "date-time",
          "description": "The date of the images. Searches the earth_date field\r\nfrom NASA.",
          "type": "string"
        },
        "savePath": {
          "description": "An optional path where to save the images.\r\nIf this field is empty, the files are saved to c:/NASA/Images/{date}",
          "type": "string"
        }
      }
    }
  }
}

```
