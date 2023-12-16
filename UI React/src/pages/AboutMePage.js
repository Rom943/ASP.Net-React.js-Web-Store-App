<<<<<<< HEAD
// AboutMePage.js
import React from 'react';
import { Card } from 'react-bootstrap';
import '../Css/aboutCss.css';
import { SiShopify } from "react-icons/si";

const AboutMePage = () => {
  const profileImage = '/images/profilepicture.png';
  const skills = ['React Web', 'JavaScript', 'Html', 'Css', 'C# - OOP', '.Net ASP core', 'Ssms'];
  const studies = [
    { degree: '.Net & React Web Full-Stack developer.', school: 'Hacker-U Academy Ramat-Gan.', year: 'April 2022 - August 2023' },

  ];



  return (
    <div className="container_style">


      <Card className="details-card ">
      <SiShopify className='logo' size={100} />
        <Card.Body>
          <Card.Title className="section-title">About The Project - WebStore</Card.Title>
          <Card.Text>
            I have developed an e-commerce website project designed to accommodate two distinct user types: Sellers and Customers.
            The platform showcases the products offered by sellers,
            allowing customers to browse and make purchases.
            Additionally, customers have the option to provide reviews for the products they have purchased,
            enhancing the overall interaction and feedback loop within the platform.
            Both Sellers and Customers have the capability to manage their accounts,
            providing a personalized user experience.
            Users can access and review their activities through the user bar, allowing for a transparent overview of their interactions on the platform.
            Sellers, in particular, possess additional functionalities, enabling them to update and delete products from their inventory,
            ensuring dynamic and efficient management of their offerings.
          </Card.Text>
        </Card.Body>
      </Card>


      <div className='sections_container'>

        <Card className="skills-card cards_style">
          <Card.Body>
            <Card.Title className="section-title">Skills</Card.Title>
            <ul className="skills-list">
              {skills.map((skill, index) => (
                <li key={index}>* {skill}</li>
              ))}
            </ul>
          </Card.Body>
        </Card>

        <Card className="studies-card cards_style">
          <Card.Body>
            <Card.Title className="section-title">Studies</Card.Title>
            {studies.map((study, index) => (
              <div key={index} className="mb-3">
                <strong>{study.degree}</strong><br />
                {study.school}<br />
                {study.year}
              </div>
            ))}
          </Card.Body>
        </Card>
      </div>
      <Card className="details-card ">
        <Card.Img src={profileImage} alt="Profile" className="img-fluid rounded-circle profile-image" />
        <Card.Body>
          <Card.Title className="section-title">About Me</Card.Title>
          <Card.Text>
            Hello! I am Roma Alexeichick, a passionate developer with experience in building web applications using React Web and .Net ASP core api.
            <br />
            The project showcased here is the culmination of my full-stack course at Hacker-U College.
            During its development, I tackled various subjects and overcame numerous challenges,
            enhancing my ability to approach tasks with greater efficiency and gaining a deeper understanding through practical application.
            I dedicated approximately four months to the project,
            investing daily in coding and learning.
            This consistent effort not only improved my coding skills but also provided valuable insights into the covered topics.
            Facing obstacles along the way, I learned to execute tasks more accurately,
            contributing to a profound understanding of the subject matter.
            I hope you enjoy exploring my project!
          </Card.Text>
        </Card.Body>
      </Card>
    </div>
  );
};

export default AboutMePage;
=======
// AboutMePage.js
import React from 'react';
import { Card } from 'react-bootstrap';
import '../Css/aboutCss.css';
import { SiShopify } from "react-icons/si";

const AboutMePage = () => {
  const profileImage = '/images/profilepicture.png';
  const skills = ['React Web', 'JavaScript', 'Html', 'Css', 'C# - OOP', '.Net ASP core', 'Ssms'];
  const studies = [
    { degree: '.Net & React Web Full-Stack developer.', school: 'Hacker-U Academy Ramat-Gan.', year: 'April 2022 - August 2023' },

  ];



  return (
    <div className="container_style">


      <Card className="details-card ">
      <SiShopify className='logo' size={100} />
        <Card.Body>
          <Card.Title className="section-title">About The Project - WebStore</Card.Title>
          <Card.Text>
            I have developed an e-commerce website project designed to accommodate two distinct user types: Sellers and Customers.
            The platform showcases the products offered by sellers,
            allowing customers to browse and make purchases.
            Additionally, customers have the option to provide reviews for the products they have purchased,
            enhancing the overall interaction and feedback loop within the platform.
            Both Sellers and Customers have the capability to manage their accounts,
            providing a personalized user experience.
            Users can access and review their activities through the user bar, allowing for a transparent overview of their interactions on the platform.
            Sellers, in particular, possess additional functionalities, enabling them to update and delete products from their inventory,
            ensuring dynamic and efficient management of their offerings.
          </Card.Text>
        </Card.Body>
      </Card>


      <div className='sections_container'>

        <Card className="skills-card cards_style">
          <Card.Body>
            <Card.Title className="section-title">Skills</Card.Title>
            <ul className="skills-list">
              {skills.map((skill, index) => (
                <li key={index}>* {skill}</li>
              ))}
            </ul>
          </Card.Body>
        </Card>

        <Card className="studies-card cards_style">
          <Card.Body>
            <Card.Title className="section-title">Studies</Card.Title>
            {studies.map((study, index) => (
              <div key={index} className="mb-3">
                <strong>{study.degree}</strong><br />
                {study.school}<br />
                {study.year}
              </div>
            ))}
          </Card.Body>
        </Card>
      </div>
      <Card className="details-card ">
        <Card.Img src={profileImage} alt="Profile" className="img-fluid rounded-circle profile-image" />
        <Card.Body>
          <Card.Title className="section-title">About Me</Card.Title>
          <Card.Text>
            Hello! I am Roma Alexeichick, a passionate developer with experience in building web applications using React Web and .Net ASP core api.
            <br />
            The project showcased here is the culmination of my full-stack course at Hacker-U College.
            During its development, I tackled various subjects and overcame numerous challenges,
            enhancing my ability to approach tasks with greater efficiency and gaining a deeper understanding through practical application.
            I dedicated approximately four months to the project,
            investing daily in coding and learning.
            This consistent effort not only improved my coding skills but also provided valuable insights into the covered topics.
            Facing obstacles along the way, I learned to execute tasks more accurately,
            contributing to a profound understanding of the subject matter.
            I hope you enjoy exploring my project!
          </Card.Text>
        </Card.Body>
      </Card>
    </div>
  );
};

export default AboutMePage;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
