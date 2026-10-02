using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Adjust these paths to match your project structure
        string[] files =
        {
            @"D:\ML-GenAI-AgenticAI\LegacyApp\LegacyApp\\EmployeeMaster.cs",
            @"D:\ML-GenAI-AgenticAI\LegacyApp\LegacyApp\\AddressMaster.cs",
            @"D:\ML-GenAI-AgenticAI\LegacyApp\LegacyApp\\DepartmentMaster.cs",
            @"D:\ML-GenAI-AgenticAI\LegacyApp\LegacyApp\\EmployeeFacade.cs"
        };

        foreach (var file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            string code = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

            Console.WriteLine($"\n=== Syntax Tree for {Path.GetFileName(file)} ===");
            PrintTree1(tree.GetRoot(), "");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
    static void PrintTree1(SyntaxNode node, string indent)
    {
        switch (node)
        {
            case ClassDeclarationSyntax classDecl:
                Console.WriteLine($"{indent}Class: {classDecl.Identifier.Text}");
                break;

            case MethodDeclarationSyntax methodDecl:
                Console.WriteLine($"{indent}Method: {methodDecl.Identifier.Text}({string.Join(", ",
                    methodDecl.ParameterList.Parameters)})");
                break;

            case ConstructorDeclarationSyntax ctorDecl:
                Console.WriteLine($"{indent}Constructor: {ctorDecl.Identifier.Text}({string.Join(", ",
                    ctorDecl.ParameterList.Parameters)})");
                break;

            case PropertyDeclarationSyntax propDecl:
                Console.WriteLine($"{indent}Property: {propDecl.Identifier.Text} : {propDecl.Type}");
                break;

            default:
                Console.WriteLine($"{indent}{node.Kind()}");
                break;
        }

        foreach (var child in node.ChildNodes())
        {
            PrintTree1(child, indent + "  ");
        }
    }
    static void PrintTree(SyntaxNode node, string indent)
    {
        Console.WriteLine($"{indent}{node.Kind()}");

        foreach (var child in node.ChildNodes())
        {
            PrintTree(child, indent + "  ");
        }
    }
}
