import * as React from "react";
import Breadcrumbs from "@mui/material/Breadcrumbs";
import TextField from "@mui/material/TextField";
import InputAdornment from "@mui/material/InputAdornment";
import Button from "@mui/material/Button";
import styles from "./AddEndPoint.module.scss";
import { EndPointHeaders, IEndPointHeader } from "./EndPointHeaders";

export const AddEndPoint = (props: IAddEndPointProps) => {
  //states
  const [endpointUrl, setEndPointUrl] = React.useState<string>("");
  const [endpointExpectedResponseBody, setEndpointExpectedResponseBody] =
    React.useState<string>("");
  const [headers, setHeaders] = React.useState<IEndPointHeader[]>([]);
  const [responseType, setResponseType] = React.useState<string>("");

  const headerCallback = React.useCallback(
    (headersParam: IEndPointHeader[]) => {
      setHeaders(headersParam);
    },
    []
  );

  const submitEndPointData = () => {
    const responseBody = {
      ResponseBody: endpointExpectedResponseBody,
      ResponseHeaders: JSON.stringify(headers),
      ResponseType: responseType,
      ContentType:"",
      ResponseStatus:""
    };

    const org = "Mocku";
    const app = "mc";

    const apiUrl =
      "http://localhost:7071/api/InsertMockAPIData/" +
      org +
      "/" +
      app +
      "/" +
      endpointUrl;

    fetch(apiUrl, {
      method: "POST",
      body: JSON.stringify(responseBody),
      headers: {
        "Content-Type": "application/json;charset=utf-8",
      },
    });
  };

  /**
   * 1. Method type
   * 2. Body
   * 3. Headers
   * Color code #f9a826 - yellow   black --#3f3d56
   */

  return (
    <>
      <div className={styles.mupageWrapper}>
        <div className={styles.mupageHeader}>../MockU</div>
        <div className={styles.mupageContents}>
          <Breadcrumbs aria-label="breadcrumb">
            <span>{props.organization}</span>
            <span>{props.app}</span>
          </Breadcrumbs>
          <div>
            <TextField
              id="tfEndpointUrl"
              label="EndPointUrl"
              variant="outlined"
              value={endpointUrl}
              onChange={(e) => {
                setEndPointUrl(e.target.value);
              }}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    {props.baseUri}
                  </InputAdornment>
                ),
              }}
            />

            <TextField
              id="tfExpectedResponseBody"
              variant="outlined"
              multiline
              label="Expected Response Body"
              value={endpointExpectedResponseBody}
              maxRows={4}
              fullWidth
              rows={4}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">{"{}"}</InputAdornment>
                ),
              }}
              onChange={(e) => {
                setEndpointExpectedResponseBody(e.target.value);
              }}
            ></TextField>
          </div>
          <div>
            <EndPointHeaders
              ExistingHeaders={[]}
              getHeadersCallback={headerCallback}
            ></EndPointHeaders>
            {/* <input type="button" onClick={}></input> */}
          </div>
          <div>
            <Button variant="contained" onClick={submitEndPointData}>
              Submit
            </Button>
            <Button variant="outlined">Clear</Button>
          </div>
        </div>
      </div>
    </>
  );
};

interface IAddEndPointProps {
  organization: string;
  app: string;
  baseUri: string;
}

/**
 * Reducer for state management
 * Material UI for ui
 */
