# TerminalNotes

tnotes --help
```
tnotes: a simple tool for managing short notes in the terminal.

Usage: tnotes [command] [options]

Commands:
  add "Note content"            Adds a new note. Note content should be in quotes.
  show [options]                Displays notes based on the provided options.
                                By default, it shows today's notes.
  search "keyword"              Searches for notes containing a specific keyword.
  --help                        Displays this help screen.
  
Options for the `show` command:
  last <number>                 Shows a specific number of the most recent notes.
                                Example: tnotes show last 3
  yesterday                     Shows notes from yesterday.
  <date>                        Shows notes from a specific date.
                                Example: tnotes show 2025-08-27
  <start_date>..<end_date>      Shows notes from a given date range.
                                Example: tnotes show 2025-08-01..2025-08-31
  week                          Shows notes from the current week.

Options for the `add` command:
  --tags <tag1>,<tag2>          Allows you to assign tags to a new note.
                                Example: tnotes add "Fix a bug" --tags #bug,#rimaster

Options for the `search` command:
  --tags <tag1>,<tag2>          Searches for notes that have the specified tags.
                                Example: tnotes search --tags #bug,#api

Examples:
  tnotes add "Prepare a report at 3 PM"
  tnotes show                   (Will display today's notes)
  tnotes show last 5
  tnotes show week
  tnotes show 2025-08-27
  tnotes show 2025-08-01..2025-08-31
  tnotes search "report"
  tnotes search --tags #bug,#api
```
