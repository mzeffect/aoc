#!/bin/bash

# Bootstrap script for creating new Advent of Code solver files
# Usage: ./bootstrap.sh 2025/Day05b

if [ -z "$1" ]; then
    echo "Usage: ./bootstrap.sh <year>/<DayXXy>"
    echo "Example: ./bootstrap.sh 2025/Day05b"
    exit 1
fi

# Parse input argument
INPUT="$1"
YEAR=$(echo "$INPUT" | cut -d'/' -f1)
DAY_PART=$(echo "$INPUT" | cut -d'/' -f2)

# Extract day number (e.g., "Day05" from "Day05b")
DAY_ID="${DAY_PART%?}"

# Validate format
if [[ ! "$DAY_PART" =~ ^Day[0-9]{2}[ab]$ ]]; then
    echo "Error: Invalid format. Expected DayXXy (e.g., Day05b)"
    exit 1
fi

# Define paths
SOLVER_DIR="Year${YEAR}"
SOLVER_FILE="${SOLVER_DIR}/${DAY_PART}.fs"
DATA_DIR="data/${YEAR}/${DAY_ID}"
INPUT_FILE="${DATA_DIR}/input.txt"
EXAMPLES_DIR="${DATA_DIR}/examples"

# Create solver file if it doesn't exist
if [ -f "$SOLVER_FILE" ]; then
    echo "Solver file already exists: $SOLVER_FILE"
else
    mkdir -p "$SOLVER_DIR"
    cat > "$SOLVER_FILE" << EOF
module AdventOfCode.Year${YEAR}.${DAY_PART}

open AdventOfCode.Utils

let solve (input: string) =
    "implement me"
EOF
    echo "Created solver file: $SOLVER_FILE"
fi

# Create data directory structure if it doesn't exist
if [ -f "$INPUT_FILE" ]; then
    echo "Input file already exists: $INPUT_FILE"
else
    mkdir -p "$DATA_DIR"
    touch "$INPUT_FILE"
    echo "Created input file: $INPUT_FILE"
fi

# Create examples directory if it doesn't exist
if [ -d "$EXAMPLES_DIR" ]; then
    echo "Examples directory already exists: $EXAMPLES_DIR"
else
    mkdir -p "$EXAMPLES_DIR"
    echo "Created examples directory: $EXAMPLES_DIR"
fi

echo ""
echo "Bootstrap complete!"
echo "Next steps:"
echo "  1. Add your puzzle input to: $INPUT_FILE"
echo "  2. Add example inputs to: $EXAMPLES_DIR"
echo "  3. Implement the solver in: $SOLVER_FILE"
echo "  4. Run with: dotnet run ${DAY_PART}"
