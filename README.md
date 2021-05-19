# BitVector and BitVectorSet
Two class libraries in order to work with BitVectors (aka BitSets): the "raw" BitVector class and the ISet abstraction: BitVectorSet.

## Usage
Install both [BitVector nuget package](https://www.nuget.org/packages/JensUngerer.BitVector) and [BitVectorSet nuget package](https://www.nuget.org/packages/JensUngerer.BitVectorSet).

```C#
using JensUngerer.BitVectorSet;
```

Derive from ``BitVectorSetFactory`` and create your custom ``factory`` class: than it is possible to create custom ``BitVectorSet`` objects.