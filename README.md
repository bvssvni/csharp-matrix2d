csharp-matrix2d
===============

Matrix library for 2D coordinate transformation.
BSD licence (see "version.md" for update log and license).

##Introduction

A matrix for 2D transformation can be described as following:

    [ a b x ]
    [ c d y ]
    [ 0 0 1 ]
    
The last row contains only constants.
This simplifies the calculations a lot.

This library uses double precision floating numbers which gives 
an accuracy suitable for animation and camera transformation.

