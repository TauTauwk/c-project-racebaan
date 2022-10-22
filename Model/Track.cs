namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section>? Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            this.Name = name;
            ConvArrayToLinkedList(sections);
        }

        private LinkedList<Section> ConvArrayToLinkedList(SectionTypes[] sectionTypes)
        {
            Sections = new LinkedList<Section>();
            foreach (SectionTypes sectionType in sectionTypes)
            {
                Sections.AddLast(new Section(sectionType));
            }

            return Sections;
        }
    }
}
