#!/bin/bash

read -p "Are you sure you want to delete all 'bin' and 'obj' folders under $(realpath ..)? (y/n) " confirmation
if [[ "$confirmation" != "y" ]]; then
    echo "Operation cancelled."
    exit 0
fi

find ../ -type d \( -name "bin" -o -name "obj" \) | while read -r dir; do
    echo "Deleting: $dir"
    if rm -rf "$dir"; then
        echo "Deleted: $dir"
    else
        echo "Failed to delete: $dir" >&2
    fi
done

echo "All 'bin' and 'obj' folders have been processed."
