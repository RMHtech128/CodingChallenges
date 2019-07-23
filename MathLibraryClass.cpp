// MathLibrary.cpp : Defines the exported functions for the DLL.
//#include "pch.h"
//#include "stdafx.h" // use pch.h in Visual Studio 2019
//#include <utility>
//#include <limits.h>
#include "pch.h"
#include "MathLibraryClass.h"


MathLibraryClass::MathLibraryClass()
{
	/*fibonacci_init(1, 1);
	bool bVal = fibonacci_next();
	unsigned long val = fibonacci_current();*/
}


MathLibraryClass::~MathLibraryClass()
{
}






// DLL internal state variables:
static unsigned long long previous_;  // Previous value, if any
static unsigned long long current_;   // Current sequence value
static unsigned index_;               // Current seq. position



// Initialize a Fibonacci relation sequence
// such that F(0) = a, F(1) = b.
// This function must be called before any other function.
void MathLibraryClass::fibonacci_init(
	const unsigned long long a,
	const unsigned long long b)
{
	index_ = 0;
	current_ = a;
	previous_ = b; // see special case when initialized
}

// Produce the next value in the sequence.
// Returns true on success, false on overflow.
bool MathLibraryClass::fibonacci_next()
{
	// check to see if we'd overflow result or position
	if ((ULLONG_MAX - previous_ < current_) ||
		(UINT_MAX == index_))
	{
		return false;
	}

	// Special case when index == 0, just return b value
	if (index_ > 0)
	{
		// otherwise, calculate next sequence value
		previous_ += current_;
	}
	std::swap(current_, previous_);
	++index_;
	return true;
}

// Get the current value in the sequence.
unsigned long long MathLibraryClass::fibonacci_current()
{
	return current_;
}

// Get the current index position in the sequence.
unsigned MathLibraryClass::fibonacci_index()
{
	return index_;
}
