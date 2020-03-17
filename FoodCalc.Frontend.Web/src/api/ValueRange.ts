interface IMultiplier {
    value: number;
    text: string;
}

export interface IValueRange {
    min: number;
    max: number;


}


export function average(range: IValueRange) { return (range.max + range.min) / 2; }

// todo: calculate on server-side
export function stringify(range: IValueRange, unit?: string): string {
    const multipliers: IMultiplier[] = [
        {
            value: 1,
            text: ""
        },
        {
            value: 0.001,
            text: "м"
        },
        {
            value: 0.000001,
            text: "мк"
        }
    ]

    const interpolate = (multiplier: IMultiplier) => range.min === range.max
        ? `${range.min * multiplier.value} ${multiplier.text}${unit || ""}`
        : `${range.min * multiplier.value}-${range.max * multiplier.value} ${multiplier.text}${unit || ""}`

    const avg = average(range);
    const mt = multipliers.find(m => m.value * avg > 0.1) || multipliers[multipliers.length - 1];

    return interpolate(mt);
}