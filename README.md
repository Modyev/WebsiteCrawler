---

# 🌐 Recursive Web Crawler in C#

A simple web crawler in C# that recursively explores links from a webpage!

## 📋 Features
- 🔍 **Parse Links**: Starts by parsing all links from a given URL.
- 🔄 **Recursive Crawling**: Visits each parsed link and extracts further links until the maximum limit is reached.
- 🔗 **Link Extraction**: Uses regular expressions to extract URLs from the page content.
- 🛡 **Duplicate Protection**: Maintains a `HashSet` of visited URLs to avoid revisiting and prevent infinite loops.
- 🎯 **Customizable**: Set the maximum number of URLs to crawl.

## 🚀 How It Works
1. **Start Crawling**: Specify the starting URL.
2. **Extract Links**: The program fetches the content of the page and extracts all links.
3. **Recursive Visits**: It recursively visits those links, repeating the process.
4. **Stop Condition**: Crawling continues until the defined maximum number of URLs is reached.

---
