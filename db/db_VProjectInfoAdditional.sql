CREATE VIEW [dbo].[VProjectInfo_Additional]
AS
SELECT *
FROM (
 SELECT p.id AS PKID, p.PrjNum, '' AS PrjInnerNum, p.PrjName, p.PrjTypeNum
  , p.BuildCorpName, p.BuildCorpCode, p.ProvinceNum, p.CityNum, p.CountyNum
  , p.PrjApprovalNum, p.PrjApprovalLevelNum, p.BuildPlanNum AS BuldPlanNum, p.ProjectPlanNum
  , CASE p.AllInvest WHEN NULL THEN p.AllInvest WHEN '' THEN NULL ELSE CONVERT(DECIMAL(15, 2), p.AllInvest) END AllInvest
  , CASE p.AllArea WHEN NULL THEN p.AllArea WHEN '' THEN NULL ELSE CONVERT(DECIMAL(15, 2), p.AllArea) END AllArea
  , p.PrjSize, p.PrjPropertyNum, p.PrjFunctionNum
  , SUBSTRING(convert(varchar(30), p.beginDete, 120), 1, 10) AS BDate
  , SUBSTRING(convert(varchar(30), p.endDate, 120), 1, 10) AS EDate
  , SUBSTRING(convert(varchar(30), GETDATE(), 120), 1, 10) AS CREATEDATE
  , '' AS UpdateFlag, '' AS sbdqbm, pa.prjpassword, pa.gyzzpl, pa.dzyx
  , pa.lxr, pa.yddh, pa.xmtz, pa.gytze, pa.gytzbl
  , pa.lxtzze, pa.programme_address
 FROM lib4.dbo.TBProjectInfo p
  LEFT JOIN lib4.dbo.TBProjectAdditionalInfo pa ON p.PrjNum = pa.prjnum
UNION ALL
 SELECT p.PKID, p.PrjNum, p.PrjInnerNum, p.PrjName, p.PrjTypeNum
  , p.BuildCorpName, p.BuildCorpCode, p.ProvinceNum, p.CityNum, p.CountyNum
  , p.PrjApprovalNum, p.PrjApprovalLevelNum, p.BuldPlanNum, p.ProjectPlanNum, p.AllInvest
  , p.AllArea, p.PrjSize, p.PrjPropertyNum, p.PrjFunctionNum
  , SUBSTRING(convert(varchar(30), p.BDate, 120), 1, 10) AS BDate
  , SUBSTRING(convert(varchar(30), p.EDate, 120), 1, 10) AS EDate
  , SUBSTRING(convert(varchar(30), p.CREATEDATE, 120), 1, 10) AS CREATEDATE
  , p.UpdateFlag, p.sbdqbm, pa.prjpassword, pa.gyzzpl, pa.dzyx
  , pa.lxr, pa.yddh, pa.xmtz, pa.gytze, pa.gytzbl
  , pa.lxtzze, pa.programme_address
 FROM WJSJZX.dbo.TBProjectInfo p
  LEFT JOIN WJSJZX.dbo.TBProjectAdditionalInfo pa ON p.PrjNum = pa.prjnum
  WHERE not p.PrjNum in (SELECT PrjNum FROM lib4.dbo.TBProjectInfo)
) aaa;