using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Controller.ProjectController
{
    public class ExportVisitor : AbstractProjectVisitor
    {
        public ExportVisitor(string projectPath)
        {
            this.projectPath = projectPath;
        }

        public override void visit(BarGraph graph)
        {
            throw new NotImplementedException();
        }
        public override void visit(DbSource source)
        {
            throw new NotImplementedException();
        }
        public override void visit(PictureMarker pictureMarker)
        {
            throw new NotImplementedException();
        }
        public override void visit(IDMarker idMarker)
        {
            throw new NotImplementedException();
        }
        public override void visit(Project project)
        {
            ProjectConfigHTML projectConfigHTML = new ProjectConfigHTML();
            ConfigFile config = new ConfigFile(new Tag("html"));
            Section headSection = new Section(new Tag("head"));
            config.AddSection(headSection);

            headSection.AddLine(new Line(new OpenTag("meta", "charset=\"UTF-8\"")));
            headSection.AddLine(new Line(new OpenTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
            headSection.AddLine(new Line(new Tag("titel", project.Name)));

            Section body = new Section(new Tag("body"));
            config.AddSection(body);

            projectConfigHTML.Write(Path.Combine(projectPath, project.Name));
        }
    }
}
