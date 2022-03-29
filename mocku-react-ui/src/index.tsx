import React from "react";
import ReactDom from "react-dom";
import styles from "./index.module.scss";
import { AddEndPoint } from "./components/AddEndPoint/AddEndPoint";

const element = React.createElement(AddEndPoint, { baseUri:"https://mocku.co.in/api/mocks/org/app/" , organization: "McDonalds" , app:"OrderForm"}); /// <div className={styles.helloWorld}>Hello Wo1rld Again!</div>;

ReactDom.render(element, document.getElementById("root"));
