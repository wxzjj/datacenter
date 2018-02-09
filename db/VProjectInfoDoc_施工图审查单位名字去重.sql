USE [WJSJZX]
GO

/****** Object:  View [dbo].[VProjectInfoDoc]    Script Date: 2018/2/9 14:24:46 ******/
DROP VIEW [dbo].[VProjectInfoDoc]
GO

/****** Object:  View [dbo].[VProjectInfoDoc]    Script Date: 2018/2/9 14:24:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VProjectInfoDoc]
AS
SELECT a.*
	,aa.DocNum
	,aa.DocCount
	,bb.ConsCorpName
	,bb.DesignCorpName
	,bb.EconCorpName
	,bb.SuperCorpName
	,cc.CensorCorpName
	,dd.EDate EDates
	,f.Jd jd1
	,f.Wd wd1
FROM TBProjectInfo a
LEFT JOIN (
	SELECT b.prjNum prjNum
		,sum(DocCount) DocCount
		,[DocNum] = stuff((
				SELECT '/' + [DocNumFrom] + ':' + [DocNumTo]
				FROM TBProjectInfoDocAdd t1
				WHERE b.prjNum = t1.prjNum
				FOR XML path('')
				), 1, 1, '')
	FROM TBProjectInfoDocAdd b
	GROUP BY b.prjNum
	) aa ON a.PrjNum = aa.prjNum
LEFT JOIN (
	SELECT c.prjNum prjNum
		,[EconCorpName] = stuff((
				SELECT '/' + [EconCorpName]
				FROM TBBuilderLicenceManage t21
				WHERE c.prjNum = t21.prjNum
				FOR XML path('')
				), 1, 1, '')
		,[DesignCorpName] = stuff((
				SELECT '/' + [DesignCorpName]
				FROM TBBuilderLicenceManage t22
				WHERE c.prjNum = t22.prjNum
				FOR XML path('')
				), 1, 1, '')
		,[ConsCorpName] = stuff((
				SELECT '/' + [ConsCorpName]
				FROM TBBuilderLicenceManage t23
				WHERE c.prjNum = t23.prjNum
				FOR XML path('')
				), 1, 1, '')
		,[SuperCorpName] = stuff((
				SELECT '/' + [SuperCorpName]
				FROM TBBuilderLicenceManage t24
				WHERE c.prjNum = t24.prjNum
				FOR XML path('')
				), 1, 1, '')
	FROM TBBuilderLicenceManage c
	GROUP BY c.prjNum
	) bb ON a.PrjNum = bb.prjNum
LEFT JOIN (
	SELECT d.prjNum prjNum
		,[CensorCorpName] = stuff((
				SELECT '/' + [CensorCorpName]
				FROM TBProjectCensorInfo t3
				WHERE d.prjNum = t3.prjNum
				GROUP BY t3.[CensorCorpName]
				FOR XML path('')
				), 1, 1, '')
	FROM TBProjectCensorInfo d
	GROUP BY d.prjNum
	) cc ON a.PrjNum = cc.prjNum
LEFT JOIN (
	SELECT e.prjNum prjNum
		,[EDate] = stuff((
				SELECT '/' + CONVERT(VARCHAR(100), [EDate], 23)
				FROM TBProjectFinishManage t4
				WHERE e.prjNum = t4.prjNum
				FOR XML path('')
				), 1, 1, '')
	FROM TBProjectFinishManage e
	GROUP BY e.prjNum
	) dd ON a.PrjNum = dd.prjNum
LEFT JOIN TBProjectInfoDoc f ON f.PrjNum = a.PrjNum
GO


