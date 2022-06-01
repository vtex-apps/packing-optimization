# Packing Optimization

## Functionalities 📦
---
- Create user defined boxes
- Pack all request body items optimally into the list of user defined boxes

## Configurations ⚙️
---
### How to *Access* Configurations
- In the Admin Panel, `Search` for `Packing Optimization`
### How to *Add* and *Remove* Your Own Boxes
- In the `Packing Optimization` app, input your *box length*, *box width*, and *box height*. Box description is optional and press `Add To Table`. The description is for your own purposes and will not be used by the app.
- To delete a box, simply press `Delete`

## Apps Using Packing Optimization 🚚
---
- FedEx App currently integrates with the Packing Optimization App. With the packing app and a diverse set of box sizes, users can experience a significant shipping cost saving.

## Things to Note
---
> For performance sake, for situations where we need multiple boxes, we're picking the box with the highest **percentItemVolumePacked**; we pick the largest **percentContainerVolumePacked** as a tiebreaker.

> 🧮 After packing, the box weights are calculated using $$Box Weight = \sum_{n=1}^n ItemWeight_n$$ With N denoting item number in box

> ⚠️ The following must all be true for an item to be packed into a box \
    1. Length<sub>Item</sub> ≤ Length<sub>Box</sub> \
    2. Width<sub>Item</sub> ≤ Width<sub>Box</sub> \
    3. Height<sub>Item</sub> ≤ Height<sub>Box</sub> 

> ⚠️ Currently, the boxes defined have an infinite quantity.

> ⚠️ Items that do not fit into any boxes will not be returned. Moreover, there must be at least one box for this app to the utilized.

> The box dimensions defined here should be in the same unit of measurement as the items defined in VTEX. For instance, if items are measured in inches, then the box values should also be in inches.


## Endpoints
---
| Field | Value |
| --- | ---|
|URL|/packAll|
|METHOD|POST|

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
---
> This app integrates: https://github.com/davidmchapman/3DContainerPacking
 
> Paper on 3D Packing Solution: https://scholar.afit.edu/cgi/viewcontent.cgi?article=5567&context=etd