import { useState } from "react";
import "./App.css";
import { Tabs, TabsTypes } from "./components/tabs/Tabs";
import { ProjectsSection } from "./sections/ProjectsSection";
import { RegistrationSection } from "./sections/RegistrationSection";
import { CreateProjectSection } from "./sections/CreateProjectSection";

function App() {
  const [selected, setSelected] = useState<TabsTypes>("Registration");

  return (
    <div className="flex flex-col p-10 items-center justify-center space-y-10 h-full">
      <p className="font-medium sm:text-7xl mb-10 text-4xl  ">Timelogger</p>
      <Tabs selected={selected} setSelected={setSelected} />
      {selected === "Registration" && <RegistrationSection />}
      {selected === "Projects" && <ProjectsSection />}
      {selected === "CreateProject" && <CreateProjectSection />}
    </div>
  );
}

export default App;
