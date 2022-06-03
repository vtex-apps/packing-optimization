📢 Use this project, [contribute](https://github.com/vtex-apps/packing-optimization) to it or open issues to help evolve it using [Store Discussion](https://github.com/vtex-apps/store-discussion).
# Packing Optimization
<!-- DOCS-IGNORE:start -->
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-0-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->
<!-- DOCS-IGNORE:end -->

The **Packing Optimization** app provides a REST API that, when provided with a list of available shipping box types and a list of items that need to be packed, will return the most optimal way of packing those items (i.e. fitting the items into the smallest possible number of smallest boxes). By using this app and a diverse set of box sizes, users can experience a significant shipping cost savings.
## Features 📦
- Create and manage a list of available box types
- Pack all provided items optimally into the available box types

## Installation
You can install the **Packing Optimization** app by running `vtex install vtex.packing-optimization` in your terminal, using the [VTEX IO CLI](https://developers.vtex.com/vtex-developer-docs/docs/vtex-io-documentation-vtex-io-cli-installation-and-command-reference).

## Configuration ⚙️
### How to *Access* Configuration
- In the VTEX Admin, `Search` for `Packing Optimization`
### Defining Access Key
- The Access Key is used to access the pack API or connecting it with another app, such as FedEx. You can define an access key and save it by pressing `Save`. If you 
### How to *Add* and *Remove* Your Own Boxes
- In the `Packing Optimization` admin panel, input your *box length*, *box width*, and *box height*. Box description is optional and for your own reference. After inputting the desired information, press `Add To Table`. Repeat as desired to register all of your box types.
- To delete a box, simply press `Delete`

## Apps Using Packing Optimization 🚚
- [FedEx Shipping](https://github.com/vtex-apps/fedex-shipping)

## Things to Note

> In situations where the app needs multiple boxes, it will pick the box with the highest **percentItemVolumePacked**. In the case of a tie, it picks the box with the largest **percentContainerVolumePacked**.

> 🧮 After packing, the box weights are calculated using $$Box Weight = \sum_{n=1}^n ItemWeight_n$$ With N denoting item number in box

> ⚠️ The following must all be true for an item to be packed into a box \
    1. Length<sub>Item</sub> ≤ Length<sub>Box</sub> \
    2. Width<sub>Item</sub> ≤ Width<sub>Box</sub> \
    3. Height<sub>Item</sub> ≤ Height<sub>Box</sub> 

> ⚠️ Currently, the app assumes that there is an infinite available quantity of each box type.

> ⚠️ Items that do not fit into any boxes will not be returned. Moreover, at least one box type must be defined for this app to be utilized.

> The box dimensions defined here should be in the same unit of measurement as the items defined in VTEX. For instance, if items are measured in inches, then the box values should also be in inches.


## Endpoints
| Field | Value |
| --- | ---|
|URL|/packAll|
|METHOD|POST|

The following headers can be used
- AccessKey: `Your Access Key`

Request Body Example
```json
{
    "ItemsToPack": [
        {
            "ID": 1,
            "Dim1": 1,
            "Dim2": 1,
            "Dim3": 1,
            "Quantity": 1
        },
        {
            "ID": 2,
            "Dim1": 1,
            "Dim2": 2,
            "Dim3": 2,
            "Quantity": 1
        }
    ]
}
```

Response Body Example
```json
{
    "packedResults": [
        {
            "containerID": 1,
            "algorithmPackingResults": [
                {
                    "algorithmID": 1,
                    "algorithmName": "EB-AFIT",
                    "isCompletePack": true,
                    "packedItems": [
                        {
                            "id": 2,
                            "isPacked": true,
                            "dim1": 1,
                            "dim2": 2,
                            "dim3": 2,
                            "coordX": 0,
                            "coordY": 0,
                            "coordZ": 0,
                            "quantity": 1,
                            "packDimX": 2,
                            "packDimY": 1,
                            "packDimZ": 2,
                            "volume": 4
                        },
                        {
                            "id": 1,
                            "isPacked": true,
                            "dim1": 1,
                            "dim2": 1,
                            "dim3": 1,
                            "coordX": 2,
                            "coordY": 0,
                            "coordZ": 0,
                            "quantity": 1,
                            "packDimX": 1,
                            "packDimY": 1,
                            "packDimZ": 1,
                            "volume": 1
                        }
                    ],
                    "packTimeInMilliseconds": 0,
                    "percentContainerVolumePacked": 2.60,
                    "percentItemVolumePacked": 100,
                    "unpackedItems": []
                }
            ]
        }
    ],
    "containers": [
        {
            "id": 1,
            "length": 4,
            "width": 6,
            "height": 8,
            "volume": 192
        },
        {
            "id": 5,
            "length": 10,
            "width": 10,
            "height": 100,
            "volume": 10000
        }
    ]
}

```

## Typings

Main Request Object
```json
{
    "ItemsToPack": [ Item ]
}
```

**Item** Object
```json
{
    "ID": number,
    "Dim1": number,
    "Dim2": number,
    "Dim3": number,
    "Quantity": number
}
```

## Credits ✨
> This app integrates: https://github.com/davidmchapman/3DContainerPacking
 
> Paper on 3D Packing Solution: https://scholar.afit.edu/cgi/viewcontent.cgi?article=5567&context=etd