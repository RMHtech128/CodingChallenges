#pragma once
class MathLibraryClass
{


friend class MathAppDirectXcpp *mcDirectX;

public:
	MathLibraryClass();
	~MathLibraryClass();

	void fibonacci_init(
		const unsigned long long a,
		const unsigned long long b);
	bool fibonacci_next();
	unsigned long long fibonacci_current();
	unsigned fibonacci_index();
};

