

$(document).ready
        (
            function() {
               parent.DialogLoading.Close();
               document.body.dir = document.MissionLocationsForm.dir;
               SetWrapper_Alert_Box(document.MissionLocationsForm.id);
               GetBoxesHeaders_MissionLocations();
               SetActionMode_MissionLocations('View');
               CacheTreeViewsSize_MissionLocations();
               Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
            }
        );
