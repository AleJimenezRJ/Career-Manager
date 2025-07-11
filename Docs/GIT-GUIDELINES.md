# Git Guidelines

## Index

<!-- TOC -->

- [Git Guidelines](#git-guidelines)
  - [Index](#index)
  - [Commits](#commits)
    - [Commit types](#commit-types)
    - [Description](#description)
    - [Body](#body)
    - [Footer](#footer)
    - [Co-author](#co-author)
    - [**Commit Example**](#commit-example)
  - [Branches](#branches)
    - [Branch Types](#branch-types)
    - [**Branch Examples**](#branch-examples)
  - [Pull Requests](#pull-requests)
    - [Assignees](#assignees)
    - [**Pull Request Template**](#pull-request-template)
  - [Bibliography](#bibliography)

<!-- /TOC -->

## Commits

Commit template:

```gherkin
<type>: <description> (<issue>)

<optional body>


<optional footer>
<optional co-author>
```

### Commit types

|Type|Description|
|-|-|
|`feat`|A new feature|
|`fix`|A bug fix|
|`docs`|Documentation only changes|
|`style`|Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)|
|`refactor`|A code change that neither fixes a bug nor adds a feature or deletes something|
|`perf`|A code change that improves performance|
|`test`|Adding missing tests or correcting existing tests|
|`ci`|Changes to our Continuous Integrations configuration files and scripts|
|`chore`|Other changes that don't modify src or test files|
|`reverts`|Reverts a previous commit|

### Description

- A properly formed Git commit `<description>` should always be able to complete the following sentence: **If applied, this commit will `<description>`**.
  - For example: **If applied, this commit will remove deprecated methods.**

- The first line of the commit `<type>: <description> (<issue>)` should always be in lowercase and present-tense.
  - Good example: **fix: show referrer for Wasm module dependency errors (#28653)**.
  - Bad example: **Changing behavior of Wasm module**. This commit does not follow the template, and it is in past-tense.

### Body

`<body>` is an optional field that should be used for a more detailed explanatory description of the change made in the commit. It also needs to be wrapped at about 72 characters per line.

If there is any pending work or additional consideration for the other developers, specify it at the end of the body. Include contact information of all
the developers that contributed as well as their percentage of attribution.

### Footer

If a commit fixes or closes the issue, `<footer>` should indicate a message ex: `Fixes #23`.

### Co-author

Co-authors should be added at the **end** of the commit message with the following template:

```sh
Co-authored-by: git_username <git_primary_email@example.com>
```

### Commit Example

```md
docs: add git guidelines

This commit adds the markdown file with the workflow description and templates to
be used in the repo

Fixes #2

Co-authored-by: git_username1 <git_primary_email@ucr.ac.cr>
Co-authored-by: git_username2 <git_primary_email@ucr.ac.cr>
Co-authored-by: git_username3 <git_primary_email@ucr.ac.cr>
```

## Branches

Branch template:

```gherkin
<type>/<team>/<description>
```

### Branch Types

|Type|Description|Example|
|-|-|-|
|`feature`|Used to develop new features, bug fixes or other changes in a separated branch from `main`|feature/add-user-authentication|
|`fix`|A bug fix applied to a *feature* branch or to the *main* branch.|fix/team/fix-buildings-read|
|`docs`|Add or edit any kind of documentation.|docs/team/git-template|
|`refactor`|A code change that neither fixes a bug nor adds a feature or deletes something|refactor/team/value-objects|
|`chore`|To complete general tasks that doesn't add a specific functionality|


### Branch Examples

Example:

```sh
# <type>/<description>
chore/code-base
```

## Pull Requests

### Assignees

Assignees clarify who is working on specific issues and pull requests.

### Pull Request Template

```git
## Description

### Type of Change

- [ ] Refactor (**non-breaking** change to the code that does not add functionality)
- [ ] Bug fix (**non-breaking** change which fixes an issue)
- [ ] New feature (**non-breaking** change which adds functionality)
- [ ] Documentation update
- [ ] Chore (**non-breaking** change to the repository structure)

### What was done?

- Briefly explain the changes introduced.
- List new features, behavior changes, or refactors.

### How Has This Been Tested? (Only for feature changes)

Describe the tests that were made to verify the changes.

- [ ] Test A
- [ ] Test B

```

## Bibliography

1. Conventional Commits cheat sheet - Kapeli. (n.d.). https://kapeli.com/cheat_sheets/Conventional_Commits.docset/Contents/Resources/Documents/index
