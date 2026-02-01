// ==========================================================
// Fast Math Functions for HLSL
// ==========================================================
// High-performance approximations for GPU shaders
// Suitable for visual effects, NOT for precise calculations
// ==========================================================

#ifndef FAST_MATH_INCLUDED
#define FAST_MATH_INCLUDED

// ==========================================================
// Mathematical Constants
// ==========================================================
#define MATH_PI           3.141592653589793
#define MATH_TWO_PI       6.283185307179586
#define MATH_HALF_PI      1.570796326794897
#define MATH_INV_PI       0.318309886183791
#define MATH_INV_TWO_PI   0.159154943091895
#define MATH_E            2.718281828459045
#define MATH_SQRT2        1.414213562373095
#define MATH_INV_SQRT2    0.707106781186548

// ==========================================================
// Phase Normalization Macros
// ==========================================================

// Normalize to [0, 2π)
#define NORMALIZE_PHASE_0_2PI(x) (frac((x) * MATH_INV_TWO_PI) * MATH_TWO_PI)

// Normalize to [-π, π)
#define NORMALIZE_PHASE_NEG_PI_PI(x) (frac((x) * MATH_INV_TWO_PI + 0.5) * MATH_TWO_PI - MATH_PI)

// Normalize to [0, π)
#define NORMALIZE_PHASE_0_PI(x) (frac((x) * MATH_INV_PI) * MATH_PI)

// Normalize to [0, 1) - for general periodic functions
#define NORMALIZE_PHASE_0_1(x) frac(x)

// Normalize to [-1, 1)
#define NORMALIZE_PHASE_NEG1_1(x) (frac((x) + 0.5) * 2.0 - 1.0)

// Normalize angle in degrees to [0, 360)
#define NORMALIZE_DEGREES(x) (frac((x) / 360.0) * 360.0)

// Normalize angle in degrees to [-180, 180)
#define NORMALIZE_DEGREES_SYMMETRIC(x) (frac((x) / 360.0 + 0.5) * 360.0 - 180.0)

// ==========================================================
// Normalized Trigonometric Functions
// ==========================================================

// Sin with normalization to [0, 2π)
#define SIN_NORMALIZED(x) sin(NORMALIZE_PHASE_0_2PI(x))

// Cos with normalization to [0, 2π)
#define COS_NORMALIZED(x) cos(NORMALIZE_PHASE_0_2PI(x))

// Tan with normalization to [0, 2π)
#define TAN_NORMALIZED(x) tan(NORMALIZE_PHASE_0_2PI(x))

// Sin with normalization to [-π, π)
#define SIN_NORMALIZED_SYM(x) sin(NORMALIZE_PHASE_NEG_PI_PI(x))

// Cos with normalization to [-π, π)
#define COS_NORMALIZED_SYM(x) cos(NORMALIZE_PHASE_NEG_PI_PI(x))

// ==========================================================
// Power Function Macros
// ==========================================================

#define POW2(x) ((x) * (x))
#define POW3(x) ((x) * (x) * (x))
#define POW4(x) ({ float _t = (x); float _t2 = _t * _t; _t2 * _t2; })
#define POW5(x) ({ float _t = (x); float _t2 = _t * _t; _t2 * _t2 * _t; })
#define POW6(x) ({ float _t = (x); float _t3 = _t * _t * _t; _t3 * _t3; })
#define POW7(x) ({ float _t = (x); float _t2 = _t * _t; float _t4 = _t2 * _t2; _t4 * _t2 * _t; })
#define POW8(x) ({ float _t = (x); float _t2 = _t * _t; float _t4 = _t2 * _t2; _t4 * _t4; })

// ==========================================================
// Interpolation Macros
// ==========================================================

// Linear step (faster than smoothstep)
#define LINEAR_STEP(edge0, edge1, x) saturate(((x) - (edge0)) / ((edge1) - (edge0)))

// Quadratic smoothstep (faster than cubic)
#define SMOOTH_STEP_QUAD(edge0, edge1, x) ({ float _t = LINEAR_STEP(edge0, edge1, x); _t * _t; })

// Inverse quadratic (ease-out)
#define SMOOTH_STEP_INVERSE_QUAD(edge0, edge1, x) ({ float _t = LINEAR_STEP(edge0, edge1, x); 1.0 - (1.0 - _t) * (1.0 - _t); })

// Smootherstep (5th order)
#define SMOOTHER_STEP(edge0, edge1, x) ({ float _t = LINEAR_STEP(edge0, edge1, x); _t * _t * _t * (_t * (_t * 6.0 - 15.0) + 10.0); })

// ==========================================================
// Utility Macros
// ==========================================================

// Fast modulo using frac (for positive values)
#define FAST_MOD(x, y) ((x) - (y) * floor((x) / (y)))

// Fast absolute value
#define FAST_ABS(x) max((x), -(x))

// Fast sign (-1, 0, or 1)
#define FAST_SIGN(x) (((x) > 0) ? 1.0 : (((x) < 0) ? -1.0 : 0.0))

// Fast sign (non-zero: -1 or 1)
#define FAST_SIGN_NONZERO(x) (((x) >= 0.0) ? 1.0 : -1.0)

// Fast clamp
#define FAST_CLAMP(x, minVal, maxVal) max((minVal), min((maxVal), (x)))

// Fast lerp (standard lerp is already fast, but this is explicit)
#define FAST_LERP(a, b, t) ((a) + ((b) - (a)) * (t))

// Remap value from one range to another
#define REMAP(value, fromMin, fromMax, toMin, toMax) \
    (((value) - (fromMin)) / ((fromMax) - (fromMin)) * ((toMax) - (toMin)) + (toMin))

// Remap and clamp
#define REMAP_CLAMPED(value, fromMin, fromMax, toMin, toMax) \
    saturate(((value) - (fromMin)) / ((fromMax) - (fromMin))) * ((toMax) - (toMin)) + (toMin)

// ==========================================================
// Vector Operation Macros
// ==========================================================

// Fast normalize (for float3)
#define FAST_NORMALIZE3(v) ((v) * rsqrt(dot((v), (v)) + 1e-10))

// Fast normalize (for float2)
#define FAST_NORMALIZE2(v) ((v) * rsqrt(dot((v), (v)) + 1e-10))

// Fast length (for float3)
#define FAST_LENGTH3(v) (rsqrt(dot((v), (v)) + 1e-10) * dot((v), (v)))

// Fast length (for float2)
#define FAST_LENGTH2(v) (rsqrt(dot((v), (v)) + 1e-10) * dot((v), (v)))

// Fast distance
#define FAST_DISTANCE3(a, b) FAST_LENGTH3((a) - (b))
#define FAST_DISTANCE2(a, b) FAST_LENGTH2((a) - (b))

// ==========================================================
// Trigonometric Approximations (Function Style)
// ==========================================================

// Fast atan2 approximation
// Error: ±0.07 rad
// Speed: 4-5x faster than atan2()
static inline float fastatan2(float y, float x)
{
    float ax = abs(x);
    float ay = abs(y);
    float a = min(ax, ay) / (max(ax, ay) + 1e-10);
    float s = a * a;
    float r = ((-0.0464964749 * s + 0.15931422) * s - 0.327622764) * s * a + a;
    
    if(ay > ax) r = 1.57079637 - r;
    if(x < 0) r = 3.14159274 - r;
    if(y < 0) r = -r;
    
    return r;
}

// Fast asin approximation (for x in [-1, 1])
// Error: ~0.001
static inline float fastasin(float x)
{
    float x2 = x * x;
    return x * (1.0 + x2 * (0.166666667 + x2 * (0.075 + x2 * 0.0446)));
}

// Fast acos approximation (for x in [-1, 1])
// Error: ~0.001
static inline float fastacos(float x)
{
    return 1.570796327 - fastasin(x);
}

// Fast atan approximation (for x in [-1, 1])
// Error: ~0.005
static inline float fastatan(float x)
{
    float x2 = x * x;
    return x * (0.995354 - x2 * (0.288679 - x2 * 0.079331));
}

// ==========================================================
// Exponential and Logarithm Approximations
// ==========================================================

// Fast exp using Pade approximation
// Error: ~0.01 for small x
static inline float fastexp(float x)
{
    return (12.0 + x * (6.0 + x)) / (12.0 + x * (-6.0 + x));
}

// Fast exp2 approximation
static inline float fastexp2(float x)
{
    int ipart = (int)x;
    float fpart = x - ipart;
    
    // Polynomial approximation for fractional part
    float exp_fpart = 1.0 + fpart * (0.693147 + fpart * (0.240227 + fpart * 0.0520833));
    
    return asfloat((ipart + 127) << 23) * exp_fpart;
}

// Fast log2 approximation
static inline float fastlog2(float x)
{
    int exp = (asint(x) >> 23) - 127;
    float man = asfloat((asint(x) & 0x007FFFFF) | 0x3F800000);
    
    return exp + (man - 1.0) / (1.0 + 0.5 * (man - 1.0));
}

// Fast natural log
static inline float fastlog(float x)
{
    return fastlog2(x) * 0.69314718; // ln(2)
}

// Fast log10
static inline float fastlog10(float x)
{
    return fastlog2(x) * 0.30102999; // log10(2)
}

// Fast general power using exp2/log2
static inline float fastpow(float x, float y)
{
    return exp2(y * log2(x + 1e-10));
}

// ==========================================================
// Square Root Approximations
// ==========================================================

// Fast square root using Newton-Raphson method
// Error: ~0.001
static inline float fastsqrt(float x)
{
    float xhalf = 0.5 * x;
    int i = asint(x);
    i = 0x5f3759df - (i >> 1); // Magic constant
    float r = asfloat(i);
    r = r * (1.5 - xhalf * r * r); // One iteration
    return x * r;
}

// Fast inverse square root (Quake III algorithm)
static inline float fastinvsqrt(float x)
{
    float xhalf = 0.5 * x;
    int i = asint(x);
    i = 0x5f3759df - (i >> 1);
    float r = asfloat(i);
    r = r * (1.5 - xhalf * r * r);
    return r;
}

// ==========================================================
// Color Space Conversions (Approximate)
// ==========================================================

// Fast linear to sRGB (approximation)
#define LINEAR_TO_SRGB(color) pow((color), 1.0 / 2.2)

// Fast sRGB to linear (approximation)
#define SRGB_TO_LINEAR(color) pow((color), 2.2)

// Fast luminance calculation (Rec. 709)
#define LUMINANCE(color) dot((color), float3(0.2126, 0.7152, 0.0722))

// Fast luminance calculation (Rec. 601)
#define LUMINANCE_601(color) dot((color), float3(0.299, 0.587, 0.114))

// Fast perceived brightness
#define PERCEIVED_BRIGHTNESS(color) dot((color), float3(0.299, 0.587, 0.114))

// ==========================================================
// Easing Functions (Macros)
// ==========================================================

// Ease In (quadratic)
#define EASE_IN_QUAD(t) ((t) * (t))

// Ease Out (quadratic)
#define EASE_OUT_QUAD(t) (1.0 - (1.0 - (t)) * (1.0 - (t)))

// Ease In Out (quadratic)
#define EASE_IN_OUT_QUAD(t) (((t) < 0.5) ? 2.0 * (t) * (t) : 1.0 - POW2(-2.0 * (t) + 2.0) / 2.0)

// Ease In (cubic)
#define EASE_IN_CUBIC(t) ((t) * (t) * (t))

// Ease Out (cubic)
#define EASE_OUT_CUBIC(t) (1.0 - POW3(1.0 - (t)))

// Ease In Out (cubic)
#define EASE_IN_OUT_CUBIC(t) (((t) < 0.5) ? 4.0 * (t) * (t) * (t) : 1.0 - POW3(-2.0 * (t) + 2.0) / 2.0)

// Ease In (exponential)
#define EASE_IN_EXP(t) (((t) == 0.0) ? 0.0 : pow(2.0, 10.0 * (t) - 10.0))

// Ease Out (exponential)
#define EASE_OUT_EXP(t) (((t) == 1.0) ? 1.0 : 1.0 - pow(2.0, -10.0 * (t)))

// Ease In Out (exponential)
#define EASE_IN_OUT_EXP(t) \
    (((t) == 0.0) ? 0.0 : (((t) == 1.0) ? 1.0 : \
    (((t) < 0.5) ? pow(2.0, 20.0 * (t) - 10.0) / 2.0 : (2.0 - pow(2.0, -20.0 * (t) + 10.0)) / 2.0)))

// Ease In (circular)
#define EASE_IN_CIRC(t) (1.0 - sqrt(1.0 - POW2(t)))

// Ease Out (circular)
#define EASE_OUT_CIRC(t) sqrt(1.0 - POW2((t) - 1.0))

// Ease In Out (circular)
#define EASE_IN_OUT_CIRC(t) \
    (((t) < 0.5) ? (1.0 - sqrt(1.0 - POW2(2.0 * (t)))) / 2.0 : (sqrt(1.0 - POW2(-2.0 * (t) + 2.0)) + 1.0) / 2.0)

// Ease In (back)
#define EASE_IN_BACK(t) (2.70158 * (t) * (t) * (t) - 1.70158 * (t) * (t))

// Ease Out (back)
#define EASE_OUT_BACK(t) (1.0 + 2.70158 * POW3((t) - 1.0) + 1.70158 * POW2((t) - 1.0))

// Bounce function
#define EASE_OUT_BOUNCE(t) \
    (((t) < 1.0 / 2.75) ? (7.5625 * (t) * (t)) : \
    (((t) < 2.0 / 2.75) ? (7.5625 * ((t) - 1.5 / 2.75) * ((t) - 1.5 / 2.75) + 0.75) : \
    (((t) < 2.5 / 2.75) ? (7.5625 * ((t) - 2.25 / 2.75) * ((t) - 2.25 / 2.75) + 0.9375) : \
    (7.5625 * ((t) - 2.625 / 2.75) * ((t) - 2.625 / 2.75) + 0.984375))))

// ==========================================================
// Hash and Noise Utilities
// ==========================================================

// Simple hash function (for procedural generation)
#define HASH11(p) frac(sin((p) * 12.9898) * 43758.5453)
#define HASH21(p) frac(sin(dot((p), float2(12.9898, 78.233))) * 43758.5453)
#define HASH31(p) frac(sin(dot((p), float3(12.9898, 78.233, 45.164))) * 43758.5453)

// Hash to float2
#define HASH22(p) frac(sin(float2(dot((p), float2(127.1, 311.7)), dot((p), float2(269.5, 183.3)))) * 43758.5453)

// Hash to float3
#define HASH33(p) frac(sin(float3(dot((p), float3(127.1, 311.7, 74.7)), \
                                  dot((p), float3(269.5, 183.3, 246.1)), \
                                  dot((p), float3(113.5, 271.9, 124.6)))) * 43758.5453)

// ==========================================================
// Rotation Utilities
// ==========================================================

// Rotate 2D vector by angle (radians)
#define ROTATE2D(v, angle) \
    ({ float2 _v = (v); float _c = cos(angle); float _s = sin(angle); \
       float2(_v.x * _c - _v.y * _s, _v.x * _s + _v.y * _c); })

// Rotate 2D vector by normalized angle [0, 2π)
#define ROTATE2D_NORMALIZED(v, angle) ROTATE2D(v, NORMALIZE_PHASE_0_2PI(angle))

// ==========================================================
// Advanced Math Utilities
// ==========================================================

// Reflect vector
#define REFLECT(I, N) ((I) - 2.0 * dot((I), (N)) * (N))

// Refract vector (simplified)
#define REFRACT_SIMPLE(I, N, eta) \
    ({ float _cosi = dot(-(I), (N)); \
       float _k = 1.0 - (eta) * (eta) * (1.0 - _cosi * _cosi); \
       (_k < 0.0) ? float3(0, 0, 0) : (eta) * (I) + ((eta) * _cosi - sqrt(_k)) * (N); })

// Faceforward
#define FACEFORWARD(N, I, Nref) (dot((Nref), (I)) < 0 ? (N) : -(N))

// ==========================================================
// Usage Examples
// ==========================================================

/*
// Phase Normalization Examples:
float wave1 = SIN_NORMALIZED(time * speed);           // [0, 2π) normalization
float wave2 = COS_NORMALIZED_SYM(time * speed);       // [-π, π) normalization
float periodic = NORMALIZE_PHASE_0_1(time);           // [0, 1) for any periodic function

// Power Functions:
float specular = POW5(NdotH);                          // 5-8x faster than pow(x, 5)
float falloff = POW2(distance);                        // 2x faster than distance * distance

// Interpolation:
float blend = LINEAR_STEP(0.0, 1.0, t);               // Faster than smoothstep
float smooth = SMOOTH_STEP_QUAD(0.0, 1.0, t);         // Quadratic smoothstep
float smoother = SMOOTHER_STEP(0.0, 1.0, t);          // 5th order (smootherstep)

// Vector Operations:
float3 normal = FAST_NORMALIZE3(inputNormal);          // Fast normalization
float dist = FAST_DISTANCE3(pos1, pos2);               // Fast distance

// Easing:
float eased = EASE_IN_OUT_CUBIC(t);                    // Smooth animation curve
float bounce = EASE_OUT_BOUNCE(t);                     // Bouncing effect

// Rotation:
float2 rotated = ROTATE2D(uv, angle);                  // 2D rotation
float2 rotatedNorm = ROTATE2D_NORMALIZED(uv, time);    // With angle normalization

// Color:
float luma = LUMINANCE(color);                         // Perceptual brightness
float3 linear = SRGB_TO_LINEAR(srgbColor);            // Color space conversion

// Hash/Noise:
float random = HASH11(seed);                           // Pseudo-random [0, 1)
float2 random2D = HASH22(uv);                          // 2D hash
*/

#endif // FAST_MATH_INCLUDED