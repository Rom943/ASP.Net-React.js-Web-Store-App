<<<<<<< HEAD
import React, { useState, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Card, ListGroup, Row, Col } from 'react-bootstrap';

const PageTracker = () => {
  // State to store the list of visited pages
  const [visitedPages, setVisitedPages] = useState([]);

  // Get the current location using useLocation from react-router-dom
  const location = useLocation();

  // Update visited pages when the location changes
  useEffect(() => {
    if (location.pathname === '/') {
      // Clear visited pages if the user is on the home page
      setVisitedPages([]);
    } else {
      // Update visited pages with the current location
      setVisitedPages((prevPages) => {
        const uniquePages = new Set([...prevPages, location.pathname]);
        return Array.from(uniquePages);
      });
    }
  }, [location.pathname]);

  return (
    <>
      {location.pathname !== '/' ? (
        <div className="container mt-4">
          <Card border="light">
            <Card.Body>
              <Card.Title className="text-muted">Visited Pages</Card.Title>
              <Row>
                <Col>
                  {/* Display visited pages as links in a horizontal list */}
                  <ListGroup horizontal>
                    {visitedPages.map((page, index) => (
                      <ListGroup.Item key={index}>
                        <Link to={page}>{page}</Link>
                      </ListGroup.Item>
                    ))}
                  </ListGroup>
                </Col>
              </Row>
            </Card.Body>
          </Card>
        </div>
      ) : (
        // Render nothing if the user is on the home page
        null
      )}
    </>
  );
};

export default PageTracker;
=======
import React, { useState, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Card, ListGroup, Row, Col } from 'react-bootstrap';

const PageTracker = () => {
  // State to store the list of visited pages
  const [visitedPages, setVisitedPages] = useState([]);

  // Get the current location using useLocation from react-router-dom
  const location = useLocation();

  // Update visited pages when the location changes
  useEffect(() => {
    if (location.pathname === '/') {
      // Clear visited pages if the user is on the home page
      setVisitedPages([]);
    } else {
      // Update visited pages with the current location
      setVisitedPages((prevPages) => {
        const uniquePages = new Set([...prevPages, location.pathname]);
        return Array.from(uniquePages);
      });
    }
  }, [location.pathname]);

  return (
    <>
      {location.pathname !== '/' ? (
        <div className="container mt-4">
          <Card border="light">
            <Card.Body>
              <Card.Title className="text-muted">Visited Pages</Card.Title>
              <Row>
                <Col>
                  {/* Display visited pages as links in a horizontal list */}
                  <ListGroup horizontal>
                    {visitedPages.map((page, index) => (
                      <ListGroup.Item key={index}>
                        <Link to={page}>{page}</Link>
                      </ListGroup.Item>
                    ))}
                  </ListGroup>
                </Col>
              </Row>
            </Card.Body>
          </Card>
        </div>
      ) : (
        // Render nothing if the user is on the home page
        null
      )}
    </>
  );
};

export default PageTracker;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
