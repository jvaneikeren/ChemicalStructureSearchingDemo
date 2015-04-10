A demonstration of using Lucene.Net and Indigo to create a basic chemical structure searching implementation. Presented at the 2015 .NET Fringe Conference.

Presentation slides are available in the "Presentation.pdf" and "Presentation.pptx" files.

Solution includes everything needed to index and search chemical structures.

For a quick searching demo, you can use either the "SearcherClient" WinForms app to search the provided small test index, or use the "SearcherWebApp" ASP.NET MVC web app to search the provided more extensive approved drugs index.

If you want to try out indexing, use the "IndexerConsole" app to create the index. This console app takes two command line arguments: [IndexDirectoryPath] and [MolFilesDirectoryPath]. You can search your newly created index easily using the "SearcherClient" WinForms app, which allows you to specify an Index Directory Path at the top of the form.
