import express from 'express';
import candidateRoutes from "./modules/candidates/candidate.routes";
import contestRoutes from "./modules/contests/contest.routes";
import errorHandler from "./middlewares/errorHandler";

const app = express();
app.use(express.json());

app.use("/api", candidateRoutes, contestRoutes);
app.use(errorHandler);

export default app;