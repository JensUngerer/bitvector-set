# BitVector and BitVectorSet
Two class libraries in order to work with BitVectors (aka BitSets): the "raw" BitVector class and the ISet abstraction: BitVectorSet.

## Usage
Install both [BitVector nuget package](https://www.nuget.org/packages/JensUngerer.BitVector/1.0.1) and [BitVectorSet nuget package](https://www.nuget.org/packages/JensUngerer.BitVectorSet/1.0.1).

```C#
using BitVectorSetLibrary;
```

Derive from ``BitVectorSetFactory`` and create your custom ``factory`` class: than it is possible to create to custom ``BitVectorSet`` objects.