# NASA Mars Rover API Integration

[![Build Status](https://redbeardss.visualstudio.com/DevOps/_apis/build/status/dst3p.nasa?branchName=master)](https://redbeardss.visualstudio.com/DevOps/_build/latest?definitionId=1&branchName=master)

An API that integrates with the NASA API to pull images taken by a rover on Mars and save them to a default or specified location.

Details of the NASA API can be found at https://api.nasa.gov/api.html#MarsPhotos

## Getting Started

### Requirements

| Tool | Location | Purpose |
|---|---| --- |
| Visual Studio | [Download 2019 Community](https://visualstudio.microsoft.com/vs/) | IDE |
| .NET 4.7.2 | [Link](https://dotnet.microsoft.com/download/dotnet-framework/net472) | SDK (Included with VS 2019 or available at the link) |
| RestSharp | Nuget | Awesome library for making REST requests
| Newtonsoft.Json | Nuget | Makes working with JSON easy
| Swashbuckle | Nuget | Creates a UI and JSON for Swagger API
| Microsoft.CodeAnalysis.FxCopAnalyzers | Nuget | Static analysis

### Clone the Repo

```bash
git clone https://github.com/dst3p/nasa.git
```

### Running Code

After installing Visual Studio with web development tools and ensuring you have the proper framework version (4.7.2), open the `.sln` file. Required nuget packages are already defined in the `packages.json` file. **Build > Build Solution** or <kbd>CTRL</kbd>+<kbd>B</kbd> will build the project and should download and restore all nuget package. If this doesn't happen manually, you can get the packages by running the following commands in the Package Manager Console (**Tools > Nuget Package Manager > Package Manager Console**):

| | |
|---|---|
| RestSharp<br />*Note: This will also download Newtonsoft.JSON* | `Install-Package RestSharp`|
| Swashbuckle | `Install-Package Swashbuckle` |
| Microsoft.CodeAnalysis.FxCopAnalyzers | `Install-Package Microsoft.CodeAnalysis.FxCopAnalyzers`|

Once these are installed, you can run the project by pressing <kbd>F5</kbd>. A webpage will load at `https://localhost/{port}/swagger` which will automatically redirect to the Swagger test harness.

Your code is now ready to make API requests.

## Sample API requests

### Get Images with Default Path

**cURL Request**

```bash
curl -H "Accept: application/json" "https://localhost:44378/nasa/photos?imageDate=2018-9-10&apiKey=ugVa3gLFzsodcHVnUinazIJStPjiPdcP6A1ivEJe&rover=curiosity"
```

**Response**

```json
{
    "location": "c:\\NASA\\Images\\20180910",
    "count": 73,
    "isSuccessful": true
}
```

### Get Images with Specified Path

**cURL Request**

```bash
curl -H "Accept: application/json" "https://localhost:44378/nasa/photos?imageDate=2018-9-10&apiKey=ugVa3gLFzsodcHVnUinazIJStPjiPdcP6A1ivEJe&rover=curiosity&savePath=c:\NASA\Derek\"
```

**Response**

```json
{
    "location": "c:\\NASA\\Derek\\20180910",
    "count": 73,
    "isSuccessful": true
}
```

## Postman

The exported Postman JSON file is available [here](Utilities/postman.json).

```json
{
  "info": {
    "_postman_id": "449ba967-3a16-4438-966d-dfcb586bdabc",
    "name": "NASA",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "NASA Image Request - Empty Request",
      "request": {
        "auth": {
          "type": "noauth"
        },
        "method": "GET",
        "header": [
          {
            "key": "Accept",
            "type": "text",
            "value": "application/json"
          }
        ],
        "url": {
          "raw": "https://localhost:44378/nasa/photos?imageDate=&apiKey=&rover=",
          "protocol": "https",
          "host": ["localhost"],
          "port": "44378",
          "path": ["nasa", "photos"],
          "query": [
            {
              "key": "imageDate",
              "value": ""
            },
            {
              "key": "apiKey",
              "value": ""
            },
            {
              "key": "rover",
              "value": ""
            }
          ]
        }
      },
      "response": []
    },
    {
      "name": "NASA Image Request",
      "request": {
        "auth": {
          "type": "noauth"
        },
        "method": "GET",
        "header": [
          {
            "key": "Accept",
            "value": "application/json",
            "type": "text"
          }
        ],
        "url": {
          "raw": "https://localhost:44378/nasa/photos?imageDate=2018-9-10&apiKey=ugVa3gLFzsodcHVnUinazIJStPjiPdcP6A1ivEJe&rover=curiosity",
          "protocol": "https",
          "host": ["localhost"],
          "port": "44378",
          "path": ["nasa", "photos"],
          "query": [
            {
              "key": "imageDate",
              "value": "2018-9-10"
            },
            {
              "key": "apiKey",
              "value": "ugVa3gLFzsodcHVnUinazIJStPjiPdcP6A1ivEJe"
            },
            {
              "key": "rover",
              "value": "curiosity"
            }
          ]
        }
      },
      "response": []
    }
  ]
}

```

## Swagger

Swagger JSON is included in the project using the Swashbuckle nuget package.

| | |
|---|---|
| Test Harness | `https://localhost:{port}/swagger` |
| JSON Document | `https://localhost:{port}/swagger/docs/v1` |


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
